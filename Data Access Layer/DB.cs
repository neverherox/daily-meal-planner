using Meal_Planner.Business_Layer;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Meal_Planner.Data_Access_Layer
{
    public class DB
    {

        XDocument xdoc = XDocument.Load("FoodProducts.xml");

        public List<Category> categories = new List<Category>();
        private Category category;
        private Product product;

        private static DB instance;
        private DB()
        {
            Read();
        }
        public static DB GetInstance()
        {
            if (instance == null)
            {
                instance = new DB();
            }
            return instance;
        }
        public void Read()
        {
            foreach (XElement catEl in xdoc.Element("Db").Elements("Category"))
            {
                category = new Category();
                category.Name = catEl.Attribute("name").Value;
                category.Description = catEl.Attribute("description").Value;
                categories.Add(category);
            }
            string name = null;
            int i = 0;
            foreach (XElement prodEl in xdoc.Element("Db").Elements("Category").Elements("Product"))
            {
                if (name != prodEl.Parent.Attribute("name").Value)
                {
                    if (name != null)
                    {
                        i++;
                    }
                    name = prodEl.Parent.Attribute("name").Value;
                }
                product = new Product();
                product.Name = prodEl.Element("Name").Value;
                product.Grams = Convert.ToInt32(prodEl.Element("Gramms").Value);
                product.Proteins = Convert.ToDouble(prodEl.Element("Protein").Value);
                product.Fats = Convert.ToDouble(prodEl.Element("Fats").Value);
                product.Carbs = Convert.ToDouble(prodEl.Element("Carbs").Value);
                product.Calories = Convert.ToDouble(prodEl.Element("Calories").Value);
                product.Category = categories[i];
                categories[i].products.Add(product);
            }
        }
        public void AddCategory(Category category)
        {
            XElement catElem = new XElement("Category",
                new XAttribute("name", category.Name),
                new XAttribute("description",category.Description));
            xdoc.Root.Add(catElem);
            xdoc.Save("FoodProducts.xml");
            categories.Add(category);
        }
        public void DeleteCategory(string name)
        {
           
            foreach (XElement xNode in xdoc.Root.Elements())
            {
                var atr = xNode.Attribute("name");
                if (atr.Value == name)
                {
                    xNode.Remove();
                }
            }
            xdoc.Save("FoodProducts.xml");
            for (int i = 0; i < categories.Count; i++)
            {
                if (categories[i].Name == name)
                {
                    categories.Remove(categories[i]);
                }
            }
        }
        public void AddProduct(Product product, string categoryName)
        {
            foreach (XElement xNode in xdoc.Root.Elements())
            {
                var atr = xNode.Attribute("name");
                if (atr.Value == categoryName)
                {
                    XElement productElem = new XElement("Product",
                        new XElement("Name", product.Name),
                        new XElement("Gramms", product.Grams),
                        new XElement("Protein", product.Proteins),
                        new XElement("Fats", product.Fats),
                        new XElement("Carbs", product.Carbs),
                        new XElement("Calories", product.Calories)
                        );
                    xNode.Add(productElem);
                }
            }
            xdoc.Save("FoodProducts.xml");
            for (int i = 0; i < categories.Count; i++)
            {
                if (categories[i].Name == category.Name)
                {
                    categories[i].products.Add(product);
                }
            }
        }
        public void DeleteProduct(string productName, string categoryName)
        {
            foreach (XElement xNode in xdoc.Root.Elements("Category").Elements("Product"))
            {
                var elem = xNode.Element("Name");
                if (elem.Value == productName)
                {
                    xNode.Remove();
                }
            }
            xdoc.Save("FoodProducts.xml");
            for (int i = 0; i < categories.Count; i++)
            {
                if (categories[i].Name == categoryName)
                {
                    for (int j = 0; j < categories[i].products.Count; j++)
                    {
                        if (categories[i].products[j].Name == productName)
                        {
                            categories[i].products.Remove(categories[i].products[j]);
                        }
                    }
                }
            }
        }
        public void SaveDailyRation(DailyRation dailyRation)
        {
            XDocument xdoc = new XDocument();
            XElement rationElem = new XElement("Рацион");
            for (int i = 0; i < dailyRation.mealTimes.Count; i++)
            {
                XElement mealElem = new XElement(dailyRation.mealTimes[i].Name);
                for (int j = 0; j < dailyRation.mealTimes[i].products.Count; j++)
                {
                    XElement productElem = new XElement("Продукт",
                        new XElement("Имя", dailyRation.mealTimes[i].products[j].Name),
                        new XElement("Граммы", dailyRation.mealTimes[i].products[j].Grams),
                        new XElement("Белки", dailyRation.mealTimes[i].products[j].Proteins),
                        new XElement("Жиры", dailyRation.mealTimes[i].products[j].Fats),
                        new XElement("Углеводы", dailyRation.mealTimes[i].products[j].Carbs),
                        new XElement("Калории", dailyRation.mealTimes[i].products[j].Calories)
                        );
                    mealElem.Add(productElem);
                }
                rationElem.Add(mealElem);
            }
            xdoc.Add(rationElem);
            xdoc.Save("DailyRation.xml");
        }
    }
}
