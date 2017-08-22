using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace TractionCalc.Models
{
    public class Dane
    {
        public virtual int ID { get; set; }
        public virtual string promien_ro { get; set; }
        public virtual string szerokosc_bo { get; set; }
        public virtual string wysH { get; set; }
        public virtual string wys_h { get; set; }
        public virtual string rd { get; set; }
        public virtual string bd { get; set; }
        public virtual string delta_h { get; set; }
        public virtual string fi { get; set; }
        public virtual string sp_c { get; set; }
        public virtual string wsp_K { get; set; }
        public virtual string wyk_n { get; set; }
        public virtual string wsp_odkrzt_k { get; set; }
        public virtual string bp { get; set; }
        public virtual string Wd { get; set; }
        public virtual string yb { get; set; }

    }

    public class DaneMap : ClassMap<Dane>
    {
        public DaneMap()
        {
            Table("Dane");

            Id(x => x.ID).GeneratedBy.Increment().Not.Nullable();
            Map(x => x.promien_ro).Column("promien_ro").Nullable();
            Map(x => x.szerokosc_bo).Column("szerokosc_bo").Nullable();
            Map(x => x.wysH).Column("wysH").Nullable();
            Map(x => x.wys_h).Column("wys_h").Nullable();
            Map(x => x.rd).Column("rd").Nullable();
            Map(x => x.bd).Column("bd").Nullable();
            Map(x => x.delta_h).Column("delta_h").Nullable();
            Map(x => x.fi).Column("fi").Nullable();
            Map(x => x.sp_c).Column("sp_c").Nullable();
            Map(x => x.wsp_K).Column("wsp_K").Nullable();
            Map(x => x.wyk_n).Column("wyk_n").Nullable();
            Map(x => x.wsp_odkrzt_k).Column("wsp_odkrzt_k").Nullable();
            Map(x => x.bp).Column("bp").Nullable();
            Map(x => x.Wd).Column("Wd").Nullable();
            Map(x => x.yb).Column("yb").Nullable();
          
        }
    }
}
