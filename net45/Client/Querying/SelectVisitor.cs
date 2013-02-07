using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Gecko.NCore.Client.Querying
{
    internal class SelectVisitor: ExpressionVisitor
    {
        private readonly List<string> _relatedObjects = new List<string>();

        public IEnumerable<string> RelatedObjects
        {
            get { return _relatedObjects.Distinct(); }
        }

        private List<string> _relatedObjectParts = new List<string>();

        protected override NewExpression VisitNew(NewExpression node)
        {
            foreach (var childNode in node.Arguments)
            {
                _relatedObjectParts = new List<string>();
                
                Visit(childNode);
                
                if (!_relatedObjectParts.Any())
                    continue;

                _relatedObjects.Add(string.Join(".", _relatedObjectParts.ToArray()));
            }

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member.MemberType != MemberTypes.Property)
                return base.VisitMember(node);

            switch (node.NodeType)
            {
                case ExpressionType.MemberAccess:
                    var propertyInfo = node.Member.DeclaringType.GetProperty(node.Member.Name);
                    if (propertyInfo.PropertyType.IsValueType)
                        break;

                    if (propertyInfo.PropertyType == typeof(string))
                        break;

                    _relatedObjectParts.Insert(0, node.Member.Name);
                    break;
            }
            return base.VisitMember(node);
        }
    }
}
