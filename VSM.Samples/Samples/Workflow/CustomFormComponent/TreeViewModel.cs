
using Jint.Native;
using Syncfusion.Maui.DataSource.Extensions;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Text.Json;


namespace VertiGIS.Mobile.Samples.Samples.Workflow.CustomFormComponent
{
    public class TreeViewModel
    {
        public ObservableCollection<TreeNode> RootNodes { get; set; } = new(); 

        public ObservableCollection<object> CheckedNodes { get; set; } = new();

        public TreeViewModel(string json)
        {
            if (!String.IsNullOrEmpty(json))
            {
                var nodes = JsonSerializer.Deserialize<List<TreeNode>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (nodes == null)
                    return;

                nodes.ForEach(RecursivelyAddNodesToCheckedNodesIfTheyAreChecked);

                if (nodes != null)
                {
                    RootNodes.Clear();
                    foreach (var node in nodes)
                    { 
                        RootNodes.Add(node);
                    }
                }

            }

        }

        private void RecursivelyAddNodesToCheckedNodesIfTheyAreChecked(TreeNode node)
        {
            if (node.IsChecked)
            {
                this.CheckedNodes.Add(node);
            }
            node.Children.ForEach(RecursivelyAddNodesToCheckedNodesIfTheyAreChecked);
        }

        public TreeViewModel()
        {
        }

    }
}
