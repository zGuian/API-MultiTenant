using GearCore.Monolith.Core.UserCore.Entities;
using GearCore.Monolith.Core.UserCore.Enums;

namespace GearCore.Monolith.Infra.Data.UserInfra.Seed
{
    public static class RoleSeed
    {
        public static IEnumerable<Roles> GetSeeds()
        {
            return
            [
                new Roles {
                    Id = "11111111-1111-1111-1111-111111111111",
                    RoleName = RolesUserEnum.ERP_ADMIN.ToString(),
                    RoleDescription = "Administrador do sistema com acesso total às funcionalidades"
                },

                new Roles{
                    Id = "22222222-2222-2222-2222-222222222222",
                    RoleName = RolesUserEnum.ERP_MANAGER.ToString(),
                    RoleDescription = "Gerente com permissão para gerenciar usuários e operações"
                },

                new Roles{
                    Id =  "33333333-3333-3333-3333-333333333333",
                    RoleName = RolesUserEnum.ERP_USER.ToString(),
                    RoleDescription = "Usuário padrão com acesso limitado às funcionalidades do sistema"
                }
            ];
        }
    }
}
