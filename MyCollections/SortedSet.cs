using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCollections
{
    public sealed class SortedSet<T> where T : IComparable<T>
    {
        internal Node<T> startNode;
        internal uint currentNumberOfNodes;

        public static implicit operator string(SortedSet<T> set)
        {
            return print(set.startNode);
        }

        private static string print(Node<T> node)
        {
            if (node == null) return "";
            return (print(node.leftChild) + " " + node.value +
                " " + print(node.rightChild)).Trim();
        }

        public uint Count
        {
            get { return currentNumberOfNodes; }
            private set
            {
                currentNumberOfNodes = value;
            }
        }

        public bool Add(T item)
        {
            if (startNode == null)
            {
                ++Count;
                startNode = new Node<T>()
                {
                    value = item
                };
                return true;
            }
            if (Add(item, startNode))
            {
                return true;
            }                
            return false;
        }

        public bool Contains(T item)
        {
            if (startNode == null)
            {
                return false;
            }
            if (Contains(item, startNode))
            {
                return true;
            }
            return false;
        }

        private bool Add(T item, Node<T> node)
        {
            if (node.value.Equals(item))
            {
                return false;
            }
            else if (item.CompareTo(node.value) < 0)
            {
                if (node.leftChild == null)
                {
                    node.leftChild = new Node<T>()
                    {
                        value = item
                    };
                    return true;
                }
                else
                {
                    return Add(item, node.leftChild);
                }
            }

            else if (item.CompareTo(node.value) > 0)
            {
                if (node.rightChild == null)
                {
                    node.rightChild = new Node<T>()
                    {
                        value = item
                    };
                    return true;
                }
                else
                {
                    return Add(item, node.rightChild);
                }
            }
            //I should never get here
            return false;
        }

        private bool Contains(T item, Node<T> node)
        {

            if (node.value.Equals(item))
            {
                return true;
            }
            else if (item.CompareTo(node.value) < 0)
            {
                if (node.leftChild == null)
                {
                    return false;
                }
                else
                {
                    return Contains(item, node.leftChild);
                }
            }

            else if (item.CompareTo(node.value) > 0)
            {
                if (node.rightChild == null)
                {
                    return false;
                }
                else
                {
                    return Contains(item, node.rightChild);
                }
            }
            //I should never get here
            return false;
        }        
    }

    internal class Node<T>
    {
        internal Node<T> leftChild, rightChild;
        internal T value;
    }
}
