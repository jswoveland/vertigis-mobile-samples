using System.Collections.Specialized;
using VertiGIS.Mobile.Workflow.Core;

namespace VertiGIS.Mobile.Samples.Samples.Workflow.CustomFormComponent
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomFormComponent : ContentComponent
    {
        public CustomFormComponent(VertiGIS.Workflow.Runtime.Definition.Forms.Element element, string name)
            : base(element, name)
        {
            var json = """
                 [
                  {
                    "Name": "Category 1",
                    "IsChecked": false,
                    "Children": [
                      {
                        "Name": "Subcategory 1.1",
                        "IsChecked": false,
                        "Children": [
                          { 
                            "Name": "Result 1.1.1",
                            "IsChecked": false,
                            "Children": [] 
                           },
                           { 
                            "Name": "Result 1.1.2",
                            "IsChecked": false,
                            "Children": [] 
                           }
                        ]
                      },
                      {
                        "Name": "Subcategory 1.2",
                        "IsChecked": false,
                        "Children": []
                      }
                    ]
                  },
                  {
                    "Name": "Category 2",
                    "IsChecked": false,
                    "Children": [
                      {
                        "Name": "Subcategory 2.1",
                        "IsChecked": false,
                        "Children": [
                          { 
                            "Name": "Result 2.1.1",
                            "IsChecked": false,
                            "Children": [] 
                           }
                        ]
                      },
                      {
                        "Name": "Subcategory 2.2",
                        "IsChecked": false,
                        "Children": [
                          { 
                            "Name": "Result 2.2.1",
                            "IsChecked": false,
                            "Children": [] 
                           },
                { 
                            "Name": "Result 2.2.3",
                            "IsChecked": false,
                            "Children": [] 
                           }
                        ]
                      }
                    ]
                  }
                ]
                """;
            TreeViewModel tvm = new TreeViewModel(json);
            this.Element.Value = tvm;
            BindingContext = this.Element.Value;
            tvm.CheckedNodes.CollectionChanged += CheckedNodes_CollectionChanged ;
            

            InitializeComponent();
        }

        private void CheckedNodes_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (TreeNode newItem in e.NewItems)
                    {
                        newItem.IsChecked = true;
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (TreeNode oldItem in e.OldItems)
                        oldItem.IsChecked = false;
                    break;

                case NotifyCollectionChangedAction.Replace:
                    Console.WriteLine("Item replaced");
                    break;

                case NotifyCollectionChangedAction.Move:
                    Console.WriteLine("Item moved");
                    break;

                case NotifyCollectionChangedAction.Reset:
                    Console.WriteLine("Collection reset");
                    break;
            }
        }

        private void treeView_ItemDoubleTapped(object sender, Syncfusion.Maui.TreeView.ItemDoubleTappedEventArgs e)
        {
            if (e.Node.Content is TreeNode node && node.IsLeaf)
            {
                node.IsChecked = !node.IsChecked;
                if (node.IsChecked)
                {
                    (this.Element.Value as TreeViewModel).CheckedNodes.Add(node);
                }
                else
                {
                    (this.Element.Value as TreeViewModel).CheckedNodes.Remove(node);
                }
            }
            else
            {
                e.Node.IsExpanded = !e.Node.IsExpanded;
            }
        }

        private void Entry_Completed(object sender, EventArgs e)
        {

        }

        private void treeView_ItemTapped(object sender, Syncfusion.Maui.TreeView.ItemTappedEventArgs e)
        {
            if (e.Node.Content is TreeNode node && node.IsLeaf)
            {
                node.IsChecked = !node.IsChecked;
                if (node.IsChecked)
                {
                    (this.Element.Value as TreeViewModel).CheckedNodes.Add(node);
                }
                else
                {
                    (this.Element.Value as TreeViewModel).CheckedNodes.Remove(node);
                }
            }
            else
            {
                e.Node.IsExpanded = !e.Node.IsExpanded;
            }
        }
    }
}
