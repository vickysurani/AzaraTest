using azara.models.Responses.Menu;

namespace azara.models.Mapping
{
    public class MenuMap
    {
        public List<MenuResponse> Map(List<MenuEntity> menuList, List<MenuEntity> defaultMenuList)
        {
            var IsChild = false;

            var response = new List<MenuResponse>();

            foreach (var menu in defaultMenuList)
            {
                if (defaultMenuList.Any(x => x.ParentId == menu.Id))
                    IsChild = true;
                else
                    IsChild = false;

                var child = new MenuResponse
                {
                    Id = menu.Id,
                    Name = menu.Name,
                    FontIconName = menu.FontIconName,
                    ParentId = menu.ParentId,
                    DefaultPermissionCsv = menu.PermissionCsv,
                    PermissionCsv = menu.PermissionCsv,
                    IsChild = IsChild,
                    PageUrl = menu.PageUrl,
                    Priority = menu.Priority,
                    IsSelected = menu.IsSelected,
                };

                if (child.IsChild)
                    child.MenuList = Map(defaultMenuList.Where(x => x.ParentId == menu.Id).ToList(), defaultMenuList);

                var existingMenu = menuList.FirstOrDefault(x => x.Name.Equals(menu.Name));
                if (existingMenu != null)
                {
                    child.PermissionCsv = existingMenu.PermissionCsv;
                    child.IsSelected = existingMenu.IsSelected;
                }

                response.Add(child);
            }

            return response;
        }
    }
}
