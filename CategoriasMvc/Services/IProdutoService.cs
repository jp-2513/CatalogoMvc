using CategoriasMvc.Models;

namespace CategoriasMvc.Services;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoViewModel>> GetProdutos();
    Task<ProdutoViewModel> GetProdutoPorId(int id);
    Task<ProdutoViewModel> CriaProduto(ProdutoViewModel produtoVM);
    Task<bool> AtualizaProduto(int id, ProdutoViewModel produtoVM);
    Task<bool> DeletaProduto(int id);
}
