using AuthenticationServices;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteTutorial4.Models
{
    public class ImageItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        // File path where the image is stored
        public string FilePath { get; set; }
        public string Caption { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
