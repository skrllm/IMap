using System;
using System.Threading.Tasks;
using IMAP.Database;
using SQLite;
using Xamarin.Forms.Maps;

namespace IMAP.Model
{
    [Table("Area")]
    public class Area
    {
        [PrimaryKey, Column("_id")]
        public Guid Id { get; set; }

        public string Label { get; set; }

        public string Description { get; set; }

        public double Latitude { get; set; }

        public double Longitide { get; set; }

        public int Radius { get; set; }

        public Area()
        {
            this.Id = Guid.NewGuid();
        }

        public Area(string Guid)
        {
            this.Id = System.Guid.Parse(Guid);
        }

        public Area Get
        {
            get => this;
        }

        public void setPosition(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitide = longitude;
        }

        public async Task SaveFromDB()
        {
            await Data.Database.SaveItemAsync<Area>(this);
        }

        public async Task DeleteFromDB()
        {
            await Data.Database.DeleteItemAsync<Area>(this);
        }
    }
}
