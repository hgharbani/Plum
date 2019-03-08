namespace Plum.Data
{
    public class FoodMaterial
    {
        public  int Id { get; set; }
        public int FoodId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int MaterialId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double UnitPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double MaterialTotalPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Material Material { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Food Food { get; set; }
    }
}
