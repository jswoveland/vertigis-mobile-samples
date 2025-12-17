using SkiaSharp;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace VertiGIS.Mobile.Samples.Samples.Workflow.CustomFormComponent
{
    enum NodeState  
    {
       Unchecked,
       Checked,
       DecendentFindings,
       DecendentsMixed
    }
    enum NodeType
    {
        Root,
        Branch,
        Leaf
    }

    public class NodeSymbol
    {
        private string symbol;
        public string Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }
        private string colour;
        public string Colour
        {
            get { return colour; }
            set { colour = value; }
        }

        public NodeSymbol(string symbol, string colour)
        {
            Symbol = symbol;
            Colour = colour;
        }
    }

    public class TreeNode : INotifyPropertyChanged
    {
        private string name = string.Empty;
        private bool isChecked = false;
        private ObservableCollection<TreeNode> children;

        public TreeNode()
        {
            children = [];
          }

       public ObservableCollection<TreeNode> Children
       {
          get { return children; }
          set
          {
             children = value;
             RaisedOnPropertyChanged("Children");
             foreach (var item in children)
             {
                item.PropertyChanged += OnChildPropertyChanged;  
             }
           }
       }

        private void OnChildPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            RaisedOnPropertyChanged(e.PropertyName);
        }

        public int CommentRowHeight
        {
            get
            {
                if (!IsLeaf)
                    return 0;
                else
                   return IsChecked ? 15 : 0;
            }
        }


        public bool IsLeaf
        {
            get
            {
                return this.children.Count == 0;
            }
        }

       public required string Name
       {
          get { return name; }
          set
          {
             name = value;
             RaisedOnPropertyChanged("Name");
          }
       }
       
        private string? comment;

        public string? Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                RaisedOnPropertyChanged("Comment");
            }
        }

        public bool CommentState
        {
            get
            {
                return this.IsChecked && this.Children.Count == 0;// ? "True" : "False";
            }
        }

        public NodeSymbol NodeSymbol
        {
            get { 
                if (this.IsLeaf)
                {
                    // Solid Circle: &#x6C                        
                    // Empty Circle: 
                    // X in box: &#FD
                    // Checked Box: &#FE
                    // Empty Check box: &#x6C
                    // Down-Right arrow &#xE6
                    // 
                    return new NodeSymbol(IsChecked ? "\u00FD" : "\u006F", IsChecked ? "Red" : "Black");
                }
                else
                {
                    return DoDecendentsHaveFindings(this) ? new NodeSymbol("\u004C", "Red") : new NodeSymbol("\u004A", "Black"); 
                }
            }
        }

        
        private static bool DoDecendentsHaveFindings(TreeNode node)
        {
            if (node.IsLeaf)
                return node.IsChecked;
            foreach (var child in node.Children)
            {
                if (DoDecendentsHaveFindings(child))
                    return true;
            }
            return false;
        }

        public string NodeSymbolColour
        {
            get { return IsChecked ? "Red" : "DarkBlue"; }
        }

        

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                RaisedOnPropertyChanged("IsChecked");
                RaisedOnPropertyChanged("CommentState");
                RaisedOnPropertyChanged("NodeSymbol");
                RaisedOnPropertyChanged("CommentRowHeight");
                
            }
        }

       public event PropertyChangedEventHandler PropertyChanged;

       public void RaisedOnPropertyChanged(string _propertyName)
       {
          if (PropertyChanged != null)
          {
             PropertyChanged(this, new PropertyChangedEventArgs(_propertyName));
          }
       }
    }

}
