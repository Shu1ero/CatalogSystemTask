using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
    public class FoldersModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ParentFolderId { get; set; }
        public List<FoldersModel>? Folders { get; set;}
    }
}
