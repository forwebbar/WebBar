using System.Collections.Generic;
using System.Linq;
using Impl.DAL;

namespace WebBar.BeerServer.DataCollector
{
    internal class DbDevice
    {
        internal long Id;
        internal int IdMarket;

        public static DbDevice Copy(DbDevice x) => new DbDevice
        {
            Id = x.Id,
            IdMarket = x.IdMarket
        };

        public void AddFills(List<Fill> fills)
        {
            using (var context = new BeerControlEntities())
            {
                foreach (var fill in fills)
                {
                    var dbFill =
                        context.Fill.FirstOrDefault(
                            f =>
                                f.idDevice == fill.idDevice && f.TapCode == fill.TapCode && f.Ts == fill.Ts &&
                                f.OperationCode == fill.OperationCode);

                    if(dbFill != null)
                        continue;

                    context.Fill.Add(fill);
                    context.SaveChanges();

                    var drink = ReadTabDrink(context, fill);
                    context.Sell.Add(new Sell
                    {
                        idDrink = drink.Id,
                        idFill = fill.id,
                        idMarket = IdMarket,
                        idPrice = drink.IdPrice,
                        Ts = fill.Ts,
                        Volume = fill.Volume,
                        Sum = (int) (((double)fill.Volume / 1000.0) * drink.PriceVal),
                        isCleaning = false
                    });

                    context.SaveChanges();
                }               
            }
        }

        private DrinkData ReadTabDrink(BeerControlEntities context, Fill fill)
        {
            var dbTap = context.DeviceTap.FirstOrDefault(t => t.idDevice == fill.idDevice && t.TapCode == fill.TapCode);
            if (dbTap == null)
                return new NullDrink();

            var dbPrice = context.Price.FirstOrDefault(d => d.idDrink == dbTap.idDrink && fill.Ts >= d.StartTs && fill.Ts < d.EndTs);
            if(dbPrice == null)
                return new NullDrink();

            return new DrinkData
            {
                Id = dbTap.idDrink ?? 0,
                IdPrice = dbPrice.id,
                PriceVal = dbPrice.Val
            };
        }
    }

    internal class DrinkData
    {
        internal virtual int Id { get; set; }
        internal virtual int IdPrice { get; set; }
        internal virtual int PriceVal { get; set; }
    }

    internal sealed class NullDrink : DrinkData
    {
        internal override int Id => 0;
        internal override int IdPrice => 0;
        internal override int PriceVal => 0;
    }
}