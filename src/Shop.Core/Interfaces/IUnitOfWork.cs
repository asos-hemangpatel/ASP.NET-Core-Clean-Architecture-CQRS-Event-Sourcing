using System.Threading.Tasks;

namespace Shop.Core.Interfaces;

public interface IUnitOfWork
{
    /// <summary>
    /// Salva todas as alterações feitas no contexto do banco de dados.
    /// </summary>
    Task SaveChangesAsync();
}