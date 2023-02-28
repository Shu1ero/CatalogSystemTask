using Services.Model;
using System.Globalization;

namespace Services
{
    public class CatalogService : ICatalogService
    {
        private readonly DBContext _dbContext;

        public CatalogService(DBContext dBContext) 
        {
            _dbContext = dBContext;
        }

        public FoldersModel GetFoldersStructure(FoldersModel parentFolder)
        {
            var folder = new FoldersModel { Folders = new List<FoldersModel>() };

            if (parentFolder.Name != null)
            {
                folder.Name = parentFolder.Name;

                parentFolder.Folders = GetFoldersById(parentFolder.Id).Folders;
            }

            foreach (var item in parentFolder.Folders)
            {
                var subFolder = GetFoldersStructure(item);
                folder.Folders.Add(subFolder);
            }

            return folder;
        }


        public FoldersModel GetFoldersById(int id)
        {
            var folder = _dbContext.Folders.Where(x=> x.Id == id).FirstOrDefault();
            var childFolders = new List<FoldersModel>();

            foreach(var item in _dbContext.Folders.Where(x => x.ParentFolderId == id).ToList())
            {
                var subFolder = new FoldersModel { 
                    Id = item.Id,
                    Name = item.Name 
                };
                childFolders.Add(subFolder);
            }


            return new FoldersModel
            {
                Id = id,
                Name = folder.Name,
                ParentFolderId= folder.ParentFolderId,
                Folders = childFolders
            };
        }

        public FoldersModel GetParentsFolders()
        {
            var parentFolders = new List<FoldersModel>();

            foreach (var item in _dbContext.Folders.Where(x => x.ParentFolderId == 0).ToList())
            {
                var subFolder = new FoldersModel
                {
                    Id = item.Id,
                    Name = item.Name
                };
                parentFolders.Add(subFolder);
            }

            return new FoldersModel
            {
                Folders = parentFolders
            };
        }

        public void SaveFolders(FoldersModel input)
        {
           var folder = new Folder();

           if(input.Name != null) 
           {
                folder.Name= input.Name;
                folder.ParentFolderId = input.ParentFolderId;

                _dbContext.Folders.Add(folder);
                _dbContext.SaveChanges();
                
           }

            if (input.Folders != null)
            {
                foreach (var subFolder in input.Folders)
                {
                    subFolder.ParentFolderId = folder.Id;
                    SaveFolders(subFolder);
                }
            }


        }
    }
}