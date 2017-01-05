﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphView
{
    internal class GremlinVOp: GremlinTranslationOperator
    {
        public List<object> VertexIdsOrElements { get; set; }
        public GremlinVOp(params object[] vertexIdsOrElements)
        {
            VertexIdsOrElements = new List<object>(vertexIdsOrElements);
        }

        public GremlinVOp(List<object> vertexIdsOrElements)
        {
            VertexIdsOrElements = vertexIdsOrElements;
        }
        public override GremlinToSqlContext GetContext()
        {
            GremlinToSqlContext inputContext = GetInputContext();

            GremlinFreeVertexVariable newVariable = new GremlinFreeVertexVariable();
            
            foreach (var id in VertexIdsOrElements)
            {
                if (id is int)
                {
                    WScalarExpression firstExpr = GremlinUtil.GetColumnReferenceExpr(newVariable.VariableName, "id");
                    WScalarExpression secondExpr = GremlinUtil.GetValueExpr(id);
                    WBooleanComparisonExpression booleanExpr = GremlinUtil.GetEqualBooleanComparisonExpr(firstExpr, secondExpr);
                    inputContext.AddPredicate(booleanExpr);
                }
            }

            inputContext.VariableList.Add(newVariable);
            inputContext.TableReferences.Add(newVariable);
            inputContext.PivotVariable = newVariable;

            return inputContext;
        }
    }
}
