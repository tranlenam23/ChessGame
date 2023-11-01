using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ChessGame.Models
{
    [Table("maps")]
    public class MapEntity
    {
        public int Idmaps { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public int Value { get; set; }
    }
}
