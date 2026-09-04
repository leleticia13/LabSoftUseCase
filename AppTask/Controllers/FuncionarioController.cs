using AppTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AppTask.Controllers;

public class FuncionarioController : Controller
{
    private readonly DbTasksContext _context;

    public FuncionarioController(DbTasksContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var funcionarios = await _context.Funcionarios
            .Include(f => f.Departamento)
            .Include(f => f.Gerente)
            .ToListAsync();

        return View(funcionarios);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var funcionario = await _context.Funcionarios
            .Include(f => f.Departamento)
            .Include(f => f.Gerente)
            .FirstOrDefaultAsync(f => f.Codigo == id);

        return funcionario == null ? NotFound() : View(funcionario);
    }

    public async Task<IActionResult> Create()
    {
        await CarregarDepartamentos();
        await CarregarGerentes();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Codigo,Nome,Cargo,DepartamentoId,CodigoGerente")] Funcionario funcionario)
    {
        ModelState.Remove(nameof(Funcionario.Departamento));
        ModelState.Remove(nameof(Funcionario.Tarefas));
        ModelState.Remove(nameof(Funcionario.Gerente));
        ModelState.Remove(nameof(Funcionario.Subordinados));

        if (funcionario.DepartamentoId <= 0)
            ModelState.AddModelError(nameof(Funcionario.DepartamentoId), "Selecione um departamento.");

        if (ModelState.IsValid)
        {
            try
            {
                _context.Funcionarios.Add(funcionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
            }
        }

        await CarregarDepartamentos(funcionario.DepartamentoId);
        await CarregarGerentes(funcionario.CodigoGerente);
        return View(funcionario);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var funcionario = await _context.Funcionarios.FindAsync(id);
        if (funcionario == null) return NotFound();

        await CarregarDepartamentos(funcionario.DepartamentoId);
        await CarregarGerentes(funcionario.CodigoGerente, funcionario.Codigo);
        return View(funcionario);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Codigo,Nome,Cargo,DepartamentoId,CodigoGerente")] Funcionario funcionario)
    {
        if (id != funcionario.Codigo) return NotFound();

        ModelState.Remove(nameof(Funcionario.Departamento));
        ModelState.Remove(nameof(Funcionario.Tarefas));
        ModelState.Remove(nameof(Funcionario.Gerente));
        ModelState.Remove(nameof(Funcionario.Subordinados));

        if (funcionario.DepartamentoId <= 0)
            ModelState.AddModelError(nameof(Funcionario.DepartamentoId), "Selecione um departamento.");

        if (funcionario.CodigoGerente == funcionario.Codigo)
            ModelState.AddModelError(nameof(Funcionario.CodigoGerente), "Um funcionário não pode ser gerente de si mesmo.");

        if (ModelState.IsValid)
        {
            try
            {
                _context.Funcionarios.Update(funcionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
            }
        }

        await CarregarDepartamentos(funcionario.DepartamentoId);
        await CarregarGerentes(funcionario.CodigoGerente, funcionario.Codigo);
        return View(funcionario);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var funcionario = await _context.Funcionarios
            .Include(f => f.Departamento)
            .Include(f => f.Gerente)
            .FirstOrDefaultAsync(f => f.Codigo == id);
        return funcionario == null ? NotFound() : View(funcionario);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var funcionario = await _context.Funcionarios.FindAsync(id);
        if (funcionario == null) return NotFound();

        var possuiTarefas = await _context.Tarefas.AnyAsync(t => t.FuncionarioId == id);
        if (possuiTarefas)
        {
            TempData["Erro"] = "Não é possível excluir este funcionário porque existem tarefas vinculadas a ele.";
            return RedirectToAction(nameof(Delete), new { id });
        }

        var possuiSubordinados = await _context.Funcionarios.AnyAsync(f => f.CodigoGerente == id);
        if (possuiSubordinados)
        {
            TempData["Erro"] = "Não é possível excluir este funcionário porque ele é gerente de outros funcionários.";
            return RedirectToAction(nameof(Delete), new { id });
        }

        _context.Funcionarios.Remove(funcionario);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task CarregarDepartamentos(int? departamentoSelecionado = null)
    {
        ViewBag.DepartamentoId = new SelectList(
            await _context.Departamentos.OrderBy(d => d.Nome).ToListAsync(),
            "Codigo", "Nome", departamentoSelecionado);
    }

    private async Task CarregarGerentes(int? gerenteSelecionado = null, int? codigoExcluido = null)
    {
        var query = _context.Funcionarios.AsQueryable();

        if (codigoExcluido.HasValue)
            query = query.Where(f => f.Codigo != codigoExcluido.Value);

        ViewBag.CodigoGerente = new SelectList(
            await query.OrderBy(f => f.Nome).ToListAsync(),
            "Codigo", "Nome", gerenteSelecionado);
    }
}