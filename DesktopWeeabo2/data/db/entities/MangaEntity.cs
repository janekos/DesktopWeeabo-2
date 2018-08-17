using DesktopWeeabo2.data.db.entities.shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.data.db.entities {

    [Table("manga_entries")]
    public class MangaEntity : BaseEntity{        

        [Column("volumes")]
        public int volumes { get; set; }

        [Column("chapters")]
        public int chapters { get; set; }       

        /*custom vars*/
        [Column("reading_status")]
        public string readingStatus { get; set; }

        [Column("read_priority")]
        public int readPriority { get; set; }

        [Column("reread_count")]
        public int rereadCount { get; set; }

        [Column("current_chapter")]
        public int currentChapter { get; set; }
    }
}
