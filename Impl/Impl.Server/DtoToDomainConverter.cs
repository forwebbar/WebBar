using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Retail.Communication;
using Contracts.Retail.Domain;

namespace Impl.Server
{
    public static class DtoToDomainConverter
    {
        public static Store ToDomain(this StoreDto dto)
        {
            var store = new Store
            {
                Id = dto.Id,
                Racks = new List<Rack>()
            };

            if (dto.RackDtos == null)
                return store;

            foreach (var rackDto in dto.RackDtos)
                store.Racks.Add(rackDto.ToDomain());

            return store;
        }

        public static Rack ToDomain(this RackDto dto)
        {
            var rack = new Rack
            {
                Id = dto.Id,
                Shelves = new List<Shelf>()
            };

            if (dto.ShelfDtos == null)
                return rack;

            foreach (var shelfDto in dto.ShelfDtos)
                rack.Shelves.Add(shelfDto.ToDomain());

            return rack;
        }

        public static Shelf ToDomain(this ShelfDto dto)
        {
            var shelf = new Shelf
            {
                Id = dto.Id = dto.Id
            };
            return shelf;
        }
    }
}
