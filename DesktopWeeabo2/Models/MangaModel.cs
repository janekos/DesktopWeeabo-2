using DesktopWeeabo2.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Models {
    [Table("manga_entries")]
    public class MangaModel : BaseModel {

        [Column("volumes")]
        public int Volumes { get; set; }

        [Column("chapters")]
        public int Chapters { get; set; }

        /*custom vars*/
        [Column("reading_status")]
        public string ReadingStatus { get; set; }

        [Column("read_priority")]
        public int ReadPriority { get; set; }

        [Column("reread_count")]
        public int RereadCount { get; set; }

        [Column("current_chapter")]
        public int CurrentChapter { get; set; }
    }
}
