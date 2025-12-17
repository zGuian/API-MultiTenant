using GearCore.Monolith.Core.TenantCore.Entities;
using GearCore.Monolith.Core.TenantCore.Interfaces.Repositories;
using GearCore.Monolith.Core.TenantCore.Services;
using Moq;

namespace GearCore.Monolith.Tests.Unit.TenantTests.Services
{
    public class TenantQueryTests
    {
        private readonly Mock<ITenantQueryRepository> _queryRepositoryMock;
        private readonly TenantQuery _tenantQuery;

        public TenantQueryTests()
        {
            _queryRepositoryMock = new Mock<ITenantQueryRepository>();
            _tenantQuery = new TenantQuery(_queryRepositoryMock.Object);
        }

        #region GetAllAsync
        [Fact]
        public async Task GetAllAsync_WhenRepositoryReturnsTenants_ShouldReturnTenantDtos()
        {
            // Arrange
            var tenants = new HashSet<Tenant>
        {
            new Tenant
            {
                Id = "tenant-1",
                Name = "Tenant One",
                Subdomain = "tenant-one"
            },
            new Tenant
            {
                Id = "tenant-2",
                Name = "Tenant Two",
                Subdomain = "tenant-two"
            }
        };

            _queryRepositoryMock
                .Setup(r => r.GetAllToHashSet(It.IsAny<CancellationToken>()))
                .ReturnsAsync(tenants);

            // Act
            var result = await _tenantQuery.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetAllAsync_WhenNoTenantsExist_ShouldReturnEmptySet()
        {
            // Arrange
            _queryRepositoryMock
                .Setup(r => r.GetAllToHashSet(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HashSet<Tenant>());

            // Act
            var result = await _tenantQuery.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
        #endregion

        #region GetByIdAsync
        [Fact]
        public async Task GetByIdAsync_WhenTenantExists_ShouldReturnMappedTenantDto()
        {
            // Arrange
            var tenant = new Tenant
            {
                Id = "tenant-1",
                Name = "Tenant One",
                Subdomain = "tenant-one"
            };

            _queryRepositoryMock
                .Setup(r => r.GetByIdAsync(tenant.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(tenant);

            // Act
            var result = await _tenantQuery.GetByIdAsync(tenant.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Tenant One", result.Name);
            Assert.Equal("tenant-one", result.Subdomain);
        }
        #endregion
    }
}
