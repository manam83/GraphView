﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphView.GremlinTranslation
{
    internal class GremlinOrderOp: GremlinTranslationOperator, IGremlinByModulating
    {
        public List<GremlinKeyword.Order> OrderList { get; set; }
        public List<string> KeyList { get; set; }
        public List<GraphTraversal2> TraversalList { get; set; }

        public GremlinOrderOp()
        {
            OrderList = new List<GremlinKeyword.Order>();
            KeyList = new List<string>();
            TraversalList = new List<GraphTraversal2>();
        }

        public override GremlinToSqlContext GetContext()
        {
            GremlinToSqlContext inputContext = GetInputContext();

            inputContext.OrderByVariable = new Tuple<GremlinVariable, OrderByRecord>(inputContext.CurrVariable, new OrderByRecord());

            foreach (var key in KeyList)
            {
                inputContext.OrderByVariable.Item2.SortOrderList.Add(GremlinUtil.GetExpressionWithSortOrder(key, GremlinKeyword.Order.Desr));
            }

            return inputContext;
        }

        public void ModulateBy()
        {
            
        }

        public void ModulateBy(GraphTraversal2 traversal)
        {
            TraversalList.Add(traversal);
        }

        public void ModulateBy(string key)
        {
            KeyList.Add(key);
        }

        public void ModulateBy(GremlinKeyword.Order order)
        {
            OrderList.Add(order);
        }
    }
}
