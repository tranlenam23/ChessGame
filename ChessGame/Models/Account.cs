using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ChessGame.Models
{
    [Table("Account")]
    public class Account
    {

        public string username { get; set; }
        public string pass { get; set; }
        public string email { get; set; }
        public string avatar { get; set; }
        public int win {  get; set; }
        public int lost { get; set; }
    }
}
