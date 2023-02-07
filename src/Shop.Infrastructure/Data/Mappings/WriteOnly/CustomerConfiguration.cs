using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities.Customer;
using Shop.Infrastructure.Data.Extensions;

namespace Shop.Infrastructure.Data.Mappings.WriteOnly;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ConfigureBaseEntity();

        builder.Property(customer => customer.FirstName)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(100);

        builder.Property(customer => customer.LastName)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(100);

        builder.Property(customer => customer.Gender)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(6)
            // Convertendo o enumerador para string ao persistir no banco de dados.
            // Ao invés de salvar o valor (ex.: 0, 1, 3), salvará o nome do enumerador, facilitando a leitura no banco.
            .HasConversion<string>();

        // Mapeamento de Objetos de Valor (ValueObject)
        builder.OwnsOne(customer => customer.Email, ownedNav =>
        {
            ownedNav.Property(email => email.Address)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(254)
                .HasColumnName(nameof(Customer.Email));

            ownedNav.HasIndex(email => email.Address)
                .IsUnique();
        });

        builder.Property(customer => customer.DateOfBirth)
            .IsRequired()
            .HasColumnType("DATE");
    }
}