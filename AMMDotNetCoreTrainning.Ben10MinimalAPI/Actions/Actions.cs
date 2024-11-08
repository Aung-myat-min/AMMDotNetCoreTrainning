namespace AMMDotNetCoreTrainning.Ben10MinimalAPI.Actions
{
    public class Actions
    {
        public static Tbl_Ben10? findById(int id, Ben10ResponseModel model)
        {
            foreach (var item in model.Tbl_Ben10)
            {
                if (item.id == id)
                {
                    return item;
                }
            }
            return null;
        }

        public static Ben10ResponseModel? replaceAlien(Tbl_Ben10 alien, Ben10ResponseModel model)
        {
            var list = model.Tbl_Ben10.ToList();
            var targetedAlien = findById(alien.id, model);
            if (targetedAlien != null)
            {
                int index = list.IndexOf(targetedAlien);
                if (index != -1)
                {
                    list[index] = alien;
                }
                model.Tbl_Ben10 = list.ToArray();
                return model;
            }
            return null;
        }

        public static Ben10ResponseModel? deleteAlien(Tbl_Ben10 alien, Ben10ResponseModel model)
        {
            var list = model.Tbl_Ben10.ToList();
            var targetedAlien = findById(alien.id, model);
            if (targetedAlien != null)
            {
                int index = list.IndexOf(targetedAlien);
                if (index != -1)
                {
                    list.RemoveAt(index);
                }
                model.Tbl_Ben10 = list.ToArray();
                return model;
            }
            return null;
        }
    }
}
