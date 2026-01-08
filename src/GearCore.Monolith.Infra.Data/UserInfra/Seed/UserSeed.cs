using GearCore.Monolith.Core.UserCore.Entities;

namespace GearCore.Monolith.Infra.Data.UserInfra.Seed
{
    internal static class UserSeed
    {
        public static IEnumerable<User> GetSeeds()
        {
            return
            [
                new User(
                firtsName: "Admin",
                lastName: "Sistema",
                email: "admin@sistema.com",
                password: "Admin@123",
                phoneNumber: "11999990000"
                ),

                new User(
                    firtsName: "João",
                    lastName: "Silva",
                    email: "joao.silva@email.com",
                    password: "Joao@123",
                    phoneNumber: "11988887777"
                ),

                new User(
                    firtsName: "Maria",
                    lastName: "Oliveira",
                    email: "maria.oliveira@email.com",
                    password: "Maria@123",
                    phoneNumber: "11977776666"
                ),

                new User(
                    firtsName: "Carlos",
                    lastName: "Pereira",
                    email: "carlos.pereira@email.com",
                    password: "Carlos@123",
                    phoneNumber: null
                ),

                new User(
                    firtsName: "Ana",
                    lastName: "Costa",
                    email: "ana.costa@email.com",
                    password: "Ana@123",
                    phoneNumber: "11966665555"
                )
            ];
        }
    }
}
