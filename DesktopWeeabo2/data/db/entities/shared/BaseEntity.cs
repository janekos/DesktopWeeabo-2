using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.data.db.entities.shared {
    public abstract class BaseEntity {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public int id { get; set; }

        [Column("mean_score")]
        public int meanScore { get; set; }

        [Column("type")]
        public string type { get; set; }

        [Column("format")]
        public string format { get; set; }

        [Column("status")]
        public string status { get; set; }

        [Column("description")]
        public string description { get; set; }

        /*problem kids*/
        [Column("genres")]
        public string genres { get; set; }

        [Column("synonyms")]
        public string synonyms { get; set; }

        [Column("title_English")]
        public string titleEnglish { get; set; }

        [Column("title_romaji")]
        public string titleRomaji { get; set; }

        [Column("title_native")]
        public string titleNative { get; set; }

        [Column("cover_image")]
        public string coverImage { get; set; }

        /*custom vars*/
        [Column("personal_score")]
        public int personalScore { get; set; }

        [Column("personal_review")]
        public string personalReview { get; set; }

        [Column("date_added")]
        public DateTime dateAdded { get; set; }
    }
}
