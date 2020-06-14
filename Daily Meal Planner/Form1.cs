using Meal_Planner.Business_Layer;
using Meal_Planner.Service_Layer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Daily_Meal_Planner
{
    public partial class Form1 : Form
    {
        IService service = new Service();
        List<Category> categories;
        Form2 form2 = new Form2();
        Form3 form3 = new Form3();
        Form4 form4 = new Form4();
        Form5 form5 = new Form5();
        TreeNode rootNode = new TreeNode("Категории");
        TreeNode catNode;
        TreeNode prodNode;
        DailyRation dailyRation = DailyRation.GetInstance();
        User user = new User();
        public Form1()
        {
            InitializeComponent();
            categories = service.GetCategories();
            treeView1.ImageList = imageList1;
            treeView1.Nodes.Add(rootNode);
            for (int i = 0; i < categories.Count; i++)
            {
                catNode = new TreeNode(categories[i].Name);
                catNode.ImageIndex = i + 1;
                catNode.SelectedImageIndex = i + 1;
                rootNode.Nodes.Add(catNode);
                for (int j = 0; j < categories[i].products.Count; j++)
                {
                    prodNode = new TreeNode(categories[i].products[j].Name);
                    prodNode.ImageIndex = i + 1;
                    prodNode.SelectedImageIndex = i + 1;
                    catNode.Nodes.Add(prodNode);
                }
            }
            radioButton1.CheckedChanged += radioButton_CheckedChanged;
            radioButton2.CheckedChanged += radioButton_CheckedChanged;
            radioButton3.CheckedChanged += radioButton_CheckedChanged;
            radioButton4.CheckedChanged += radioButton_CheckedChanged;
            treeView2.NodeMouseClick += treeView2_NodeMouseClick;
            button8.Click += ProductStatusClear;
            treeView2.DragDrop += CreateDailyRation;
            treeView2.DragDrop += CaloriesWithProgress;
            trackBar1.Scroll += CaloriesWithProgressByWeight;
        }

        private void Button1_Click(object sender, System.EventArgs e)
        {
            form2.ShowDialog();
            Category category = new Category();
            category.Name = form2.textBox1.Text;
            category.Description = form2.textBox2.Text;
            if (category.IsValid())
            {
                service.AddCategory(category);
                catNode = new TreeNode(category.Name);
                rootNode.Nodes.Add(catNode);
            }
            else
            {
                string Errors = null;
                for (int i = 0; i < category.Errors.Count; i++)
                {
                    Errors += category.Errors[i] + "\n";
                }
                MessageBox.Show(Errors);
            }
        }

        private void Button2_Click(object sender, System.EventArgs e)
        {
            form3.ShowDialog();
            Product product = new Product();
            string categoryName = null;
            try
            {

                product.Name = form3.textBox1.Text;
                categoryName = form3.textBox2.Text;
                product.Grams = Convert.ToInt32(form3.textBox3.Text);
                product.Proteins = Convert.ToDouble(form3.textBox4.Text);
                product.Fats = Convert.ToDouble(form3.textBox5.Text);
                product.Carbs = Convert.ToDouble(form3.textBox6.Text);
                product.Calories = Convert.ToDouble(form3.textBox7.Text);
                product.Category = service.GetCategory(categoryName);
            }
            catch
            {

            }
            if (product.IsValid())
            {
                service.AddProduct(product, categoryName);
                prodNode = new TreeNode(product.Name);
                for (int i = 0; i < rootNode.Nodes.Count; i++)
                {
                    if (rootNode.Nodes[i].Text == categoryName)
                    {
                        rootNode.Nodes[i].Nodes.Add(prodNode);
                    }
                }
            }
            else
            {
                string Errors = null;
                for (int i = 0; i < product.Errors.Count; i++)
                {
                    Errors += product.Errors[i] + "\n";
                }
                MessageBox.Show(Errors);
            }
        }

        private void Button3_Click(object sender, System.EventArgs e)
        {
            form4.ShowDialog();
            string name = form4.textBox1.Text;
            service.DeleteCategory(name);
            for (int i = 0; i < rootNode.Nodes.Count; i++)
            {
                if (rootNode.Nodes[i].Text == name)
                {
                    rootNode.Nodes.Remove(rootNode.Nodes[i]);
                }
            }
        }

        private void Button4_Click(object sender, System.EventArgs e)
        {
            form5.ShowDialog();
            string productName = form5.textBox1.Text;
            string categoryName = form5.textBox2.Text;
            service.DeleteProduct(productName, categoryName);
            for (int i = 0; i < rootNode.Nodes.Count; i++)
            {
                if (rootNode.Nodes[i].Text == categoryName)
                {
                    for (int j = 0; j < rootNode.Nodes[i].Nodes.Count; j++)
                    {
                        if (rootNode.Nodes[i].Nodes[j].Text == productName)
                        {
                            rootNode.Nodes[i].Nodes.Remove(rootNode.Nodes[i].Nodes[j]);
                        }
                    }
                }
            }
        }

        private void Button5_Click(object sender, System.EventArgs e)
        {
            string SearchText = this.textBox1.Text;
            if (SearchText == "")
            {
                return;
            };
            TreeNode SelectedNode = SearchNode(SearchText, treeView1.Nodes[0]);
            if (SelectedNode != null)
            {
                this.treeView1.SelectedNode = SelectedNode;
                this.treeView1.SelectedNode.Expand();
                this.treeView1.Select();
            }
        }
        private TreeNode SearchNode(string SearchText, TreeNode StartNode)
        {
            TreeNode node = null;
            while (StartNode != null)
            {
                if (StartNode.Text.ToLower().Contains(SearchText.ToLower()))
                {
                    node = StartNode; 
                    break;
                };
                if (StartNode.Nodes.Count != 0) 
                {
                    node = SearchNode(SearchText, StartNode.Nodes[0]);
                    if (node != null)
                    {
                        break;
                    };
                };
                StartNode = StartNode.NextNode;
            };
            return node;
        }

        private void TreeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Copy);
            }
        }

        private void TreeView2_DragOver(object sender, DragEventArgs e)
        {
            Point targetPoint = treeView1.PointToClient(new Point(e.X, e.Y));
            treeView1.SelectedNode = treeView1.GetNodeAt(targetPoint);
        }
        private void TreeView2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }
        private void TreeView2_DragDrop(object sender, DragEventArgs e)
        {
            Point targetPoint = treeView2.PointToClient(new Point(e.X, e.Y));

            TreeNode targetNode = treeView2.GetNodeAt(targetPoint);

            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
            try
            {
                if (draggedNode.Parent != rootNode)
                {
                    if (e.Effect == DragDropEffects.Copy)
                    {
                        if (targetNode.Parent == null)
                        {
                            targetNode.Nodes.Add((TreeNode)draggedNode.Clone());
                            for (int i = 0; i < treeView2.Nodes.Count; i++)
                            {
                                for (int j = 0; j < treeView2.Nodes[i].Nodes.Count; j++)
                                {
                                    treeView2.Nodes[i].Nodes[j].ImageIndex = 3;
                                    treeView2.Nodes[i].Nodes[j].SelectedImageIndex = 3;
                                    treeView2.Nodes[i].Nodes[j].BackColor = Color.White;
                                }
                            }
                        }
                    }
                }
                targetNode.Expand();
            }
            catch
            {

            }
        }
        private void CreateDailyRation(object sender, DragEventArgs e)
        {
            for (int i = 0; i < dailyRation.mealTimes.Count; i++)
            {
                dailyRation.mealTimes[i].products.Clear();
            }
            for (int i = 0; i < treeView2.Nodes.Count; i++)
            {
                for (int j = 0; j < treeView2.Nodes[i].Nodes.Count; j++)
                {
                    Product product = service.GetProduct(treeView2.Nodes[i].Nodes[j].Text);
                    if (i == 0)
                    {
                        service.AddProductToMealTime(product, dailyRation.mealTimes[i]);
                    }
                    else if (i == 1)
                    {
                        service.AddProductToMealTime(product, dailyRation.mealTimes[i]);
                    }
                    else
                    {
                        service.AddProductToMealTime(product, dailyRation.mealTimes[i]);
                    }
                }
            }
        }
        private void Button7_Click(object sender, EventArgs e)
        {
            try
            {
                user.Age = Convert.ToInt32(textBox2.Text);
                user.Height = Convert.ToDouble(textBox3.Text);
                user.Weight = Convert.ToDouble(textBox4.Text);
                textBox5.Text = Convert.ToString(service.DailyColoriesRate(user));
            }
            catch
            {

            }
        }
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked)
            {
                user.Activity = radioButton.Text;
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < treeView2.Nodes.Count; i++)
            {
                    treeView2.Nodes[i].Nodes.Clear();
            }
            progressBar1.Value = 0;
            progressBar1.SetState(1);
            textBox12.Clear();
            try
            {
                for (int i = 0; i < dailyRation.mealTimes.Count; i++)
                {
                    dailyRation.mealTimes[i].products.Clear();
                }
            }
            catch
            {

            }
        }
        private void treeView2_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Parent != null)
            {
                Product product = service.GetProduct(e.Node.Text);
                label8.ForeColor = Color.Red;
                label8.Text = product.Name;
                textBox11.Text = Convert.ToString(product.Proteins);
                textBox7.Text = Convert.ToString(product.Fats);
                textBox8.Text = Convert.ToString(product.Carbs);
                textBox9.Text = Convert.ToString(product.Calories);
                textBox10.Text = Convert.ToString(product.Grams);
            }
        }
        private void ProductStatusClear(object sender, EventArgs e)
        {
            label8.ForeColor = Color.Lime;
            label8.Text = "Продукт не выбран";
            textBox11.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            try
            {
                Product product = service.GetProduct(label8.Text);
                service.ChangeProductCharacs(dailyRation, product, trackBar1.Value);
                for (int i = 0; i < dailyRation.mealTimes.Count; i++)
                {
                    for (int j = 0; j < dailyRation.mealTimes[i].products.Count; i++)
                    {
                        if (product.Name == dailyRation.mealTimes[i].products[j].Name)
                        {
                            textBox9.Text = Convert.ToString(dailyRation.mealTimes[i].products[j].Calories);
                            textBox10.Text = Convert.ToString(dailyRation.mealTimes[i].products[j].Grams);
                        }
                    }
                }
            }
            catch
            {

            }
        }
        private void CaloriesWithProgress(object sender, DragEventArgs e)
        {
            progressBar1.SetState(1);
            progressBar1.Step = 0;
            progressBar1.Value = 0;
            for (int i = 0; i < dailyRation.mealTimes.Count; i++)
            {
                for (int j = 0; j < dailyRation.mealTimes[i].products.Count; j++)
                {
                    progressBar1.Step += Convert.ToInt32(dailyRation.mealTimes[i].products[j].Calories);
                }
            }
            textBox12.Text = Convert.ToString(progressBar1.Step);
            progressBar1.PerformStep();
            if (service.DailyColoriesRate(user) <= Convert.ToDouble(textBox12.Text))
            {
                progressBar1.SetState(2);
            }
        }
        private void CaloriesWithProgressByWeight(object sender, EventArgs e)
        {
            progressBar1.SetState(1);
            progressBar1.Step = 0;
            progressBar1.Value = 0;
            for (int i = 0; i < dailyRation.mealTimes.Count; i++)
            {
                for (int j = 0; j < dailyRation.mealTimes[i].products.Count; j++)
                {
                    progressBar1.Step += Convert.ToInt32(dailyRation.mealTimes[i].products[j].Calories);
                }
            }
            textBox12.Text = Convert.ToString(progressBar1.Step);
            progressBar1.PerformStep();
            if (service.DailyColoriesRate(user) <= Convert.ToDouble(textBox12.Text))
            {
                progressBar1.SetState(2);
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            try
            {
                service.SaveDailyRation(dailyRation);
            }
            catch
            {

            }
        }
    }
}
public static class ModifyProgressBarColor
{
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
    static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
    public static void SetState(this ProgressBar pBar, int state)
    {
        SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
    }
}

