using System;
using SQLite;

namespace br.com.weblayer.logistica.android.exp.Core.Model
{
    [Table("Entrega")]
    public class Entrega
    {
        [PrimaryKey, AutoIncrement]
        public virtual int id
        { get; set; }

        [MaxLength(200), NotNull]
        public virtual string ds_NFE
        { get; set; }

        [NotNull]
        public virtual int id_ocorrencia
        { get; set; }

        [NotNull]
        public virtual DateTime? dt_inclusao
        { get; set; }

        [NotNull]
        public virtual DateTime? dt_entrega
        { get; set; }

        [MaxLength(200)]
        public virtual string ds_observacao
        { get; set; }

        public byte[] Image { get; set; }

        [MaxLength(400)]
        public virtual string ds_ImageUri
        { get; set; }

    }
}