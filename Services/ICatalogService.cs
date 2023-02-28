using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICatalogService
    {
        public FoldersModel GetFoldersById(int id);

        public FoldersModel GetParentsFolders();

        public FoldersModel GetFoldersStructure(FoldersModel parentFolder);

        public void SaveFolders(FoldersModel input);
    }
}
