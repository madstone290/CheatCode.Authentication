using EFCore.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace EFCore.Core.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Tags)
                .HasConversion(new ValueConverter<List<string>, string>(
                    value => JsonSerializer.Serialize(value, new JsonSerializerOptions()),
                    providerValue => JsonSerializer.Deserialize<List<string>>(providerValue, new JsonSerializerOptions()))
                );
        }
    }
}
