using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Data.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestSQLiteGroupBy.Data;

namespace TestSQLiteGroupBy
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService : Component
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        //public async Task<IEnumerable<IGrouping<GooStateEnum, Book1MiddleRow>>> WebMethod2()
        //public async Task<IEnumerable<XGrouping>> WebMethod2()
        public async Task<IEnumerable<Book1MiddleAsGroupByGooWithCountRow>> WebMethod2()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/201405

            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteEnumWhere\TestSQLiteEnumWhere\ApplicationWebService.cs
            // can we do sql group by or even send it over to client if we do it in memory?

            // how can this type define it depends on another assembly so 
            // security can detect it fast?
            // like throws for java.
            // http://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.dependencyattribute.aspx
            // we will need to reference a specific type on that third party.
            // we can initally do it only for assetslibrary genererated types?
            // http://stackoverflow.com/questions/473575/unity-framework-dependencyattribute-only-works-for-public-properties


            //[FileNotFoundException]: Could not load file or assembly &#39;System.Data.XSQLite, Version=3.7.7.1, Culture=neutral, PublicKeyToken=null&#39; or one of its dependencies. The system cannot find the file specified.
            //   at ScriptCoreLib.Shared.Data.Diagnostics.InternalWithConnectionLambda.WithConnection(String DataSource)
            //   at ScriptCoreLib.Shared.Data.Diagnostics.WithConnectionLambda.WithConnection(String DataSource)
            //   at TestSQLiteGroupBy.Data.Book1.Middle..ctor(String DataSource)

            var x = new Book1.Middle
            {
                // collection initializer?
                // .Add ?
            };

            var SpecialRatio = new Random().NextDouble();

            x.Insert(
                new Book1MiddleRow
                {
                    // enum name clash? 
                    FooStateEnum = FooStateEnum.Foo0,
                    GooStateEnum = GooStateEnum.Goo2,

                    Ratio = SpecialRatio,
                    x = 0.9,

                    Title = "first 0.9"
                }
            );

            x.Insert(
                new Book1MiddleRow
                {
                    // enum name clash? 
                    FooStateEnum = FooStateEnum.Foo0,
                    GooStateEnum = GooStateEnum.Goo2,

                    Ratio = SpecialRatio,
                    x = 0.8,

                    Title = "second 0.8"
                }
            );

            x.Insert(
                new Book1MiddleRow
                {
                    // enum name clash? 
                    FooStateEnum = FooStateEnum.Foo0,
                    GooStateEnum = GooStateEnum.Goo2,

                    Ratio = SpecialRatio,
                    x = 0.6,

                    Title = "third 0.6"
                }
            );



            x.Insert(
                new Book1MiddleRow
                {
                    FooStateEnum = FooStateEnum.Foo0,
                    GooStateEnum = GooStateEnum.Goo0,
                    Ratio = 0.5,

                    Title = "goo0 " + new { Count = new Book1.Middle().Count(k => k.GooStateEnum == GooStateEnum.Goo0) }
                }
            );



            // x:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Shared\Data\Diagnostics\QueryStrategyExtensions.cs
            // http://msdn.microsoft.com/en-US/vstudio/ee908647.aspx#leftouterjoin
            // f you want a LEFT OUTER JOIN then you need to use "into":
            // http://stackoverflow.com/questions/1092562/left-join-in-linq


            //MutableWhere { Method = , NodeType = Equal, ColumnName = FooStateEnum, Right = 0 }
            //AsDataTable
            //MutableWhere { n = @arg0, r = 0 }
            //select `Key`, `Title`, `Ratio`, `FooStateEnum`, `GooStateEnum`, `Tag`, `Timestamp`
            //from `Book1.Middle`
            // where `FooStateEnum` = @arg0



            // http://stackoverflow.com/questions/710508/how-best-to-loop-over-a-batch-of-results-with-a-c-sharp-dbdatareader
            // C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe  -encoding UTF-8 -cp Y:\TestSQLiteGroupBy.ApplicationWebService\staging.java\web\release;C:\util\appengine-java-sdk-1.8.8\lib\impl\*;C:\util\appengine-java-sdk-1.8.8\lib\shared\* -d "Y:\TestSQLiteGroupBy.ApplicationWebService\staging.java\web\release" @"Y:\TestSQLiteGroupBy.ApplicationWebService\staging.java\web\files"

            var g = from z in x


                    where z.FooStateEnum == FooStateEnum.Foo0
                    where z.Ratio == SpecialRatio

                    //where z.Ratio > 0.1
                    //where z.Ratio < 0.9
                    //where z.GooStateEnum == GooStateEnum.Goo2
                    //select z

                    group z by z.GooStateEnum into GroupByGoo




                    select new Book1MiddleAsGroupByGooWithCountRow
                    {
                        // GroupByGoo cannot have 0 members

                        GooStateEnum = GroupByGoo.Key,


                        Count = GroupByGoo.Count(),


                        // do we have to do multple from clauses for ordering first and last?

                        FirstTitle = GroupByGoo.First().Title,
                        FirstKey = GroupByGoo.First(),
                        Firstx = GroupByGoo.First().x,

                        LastKey = GroupByGoo.Last(),
                        LastTitle = GroupByGoo.Last().Title,
                        Lastx = GroupByGoo.Last().x,

                        SumOfx = GroupByGoo.Sum(u => u.x),


                        // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs
                        //Tag = "",
                        Tag = GroupByGoo.Last().Tag,
                        Timestamp = GroupByGoo.Last().Timestamp,

                        // count is easy what about
                        // getting the first or last items?

                    };

            // http://friism.com/linq-to-sql-group-by-subqueries-and-performance
            // !! This is because there is no good translation of such queries to SQL and Linq-to-SQL has to resort to doing multiple subqueries. 

            ;

            // Error	5	Could not find an implementation of the query pattern for source type 'TestSQLiteGroupBy.Data.Book1.Middle'.  'GroupBy' not found.	X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteGroupBy\TestSQLiteGroupBy\ApplicationWebService.cs	93	31	TestSQLiteGroupBy





            // http://www.viblend.com/Questions/WinForms/HowToGroupWinFormsDataGridViewByColumn.aspx
            // tooltips?

            //var d = g.Join
            var zzz = g.AsEnumerable();

            return zzz;

            //var u
            //var u = QueryStrategyExtensions.AsDataTable(g);


            //var g = from z in zzz
            //        group z by z.GooStateEnum;

            // 0380:01:01 RewriteToAssembly error: System.NotImplementedException: { SourceType = System.Linq.IGrouping`2[TestSQLiteGroupBy.Data.GooStateEnum,TestSQLiteGroupBy.Data.Book1MiddleRow] }

            //throw null;

            //return g.Select(gg => new XGrouping { Key = gg.Key, Items = gg.AsEnumerable() });
        }

    }


    [Obsolete("there is no good translation of such queries to SQL and Linq-to-SQL has to resort to doing multiple subqueries.")]
    public interface IQueryStrategyGroupingBuilder<TKey, TSource>
    {
        // GroupByBuilder

        IQueryStrategy<TSource> source { get; set; }
        Expression<Func<TSource, TKey>> keySelector { get; set; }
    }

    [Obsolete("group by . into .")]
    class XQueryStrategyGroupingBuilder<TKey, TSource> : IQueryStrategyGroupingBuilder<TKey, TSource>
    {
        public IQueryStrategy<TSource> source { get; set; }
        public Expression<Func<TSource, TKey>> keySelector { get; set; }
    }

    [Obsolete("to make intellisense happy, and dispay only supported methods")]
    //public interface IQueryStrategyGrouping<out TKey, out TElement> : IQueryStrategy<TElement>
    public interface IQueryStrategyGrouping<out TKey, TElement> : IQueryStrategy<TElement>
    {
        TKey Key { get; }
    }



    public static class X
    {
        [Obsolete("non grouping methods shall use FirstOrDefault")]
        public static TElement First<TKey, TElement>(this IQueryStrategyGrouping<TKey, TElement> source)
        {
            throw new NotImplementedException();
        }

        [Obsolete("non grouping methods shall use FirstOrDefault")]
        public static TElement Last<TKey, TElement>(this IQueryStrategyGrouping<TKey, TElement> source)
        {
            throw new NotImplementedException();
        }

        public static long Sum<TKey, TElement>(this IQueryStrategyGrouping<TKey, TElement> source, Expression<Func<TElement, long>> f)
        {
            throw new NotImplementedException();
        }

        public static double Sum<TKey, TElement>(this IQueryStrategyGrouping<TKey, TElement> source, Expression<Func<TElement, double>> f)
        {
            throw new NotImplementedException();
        }


        #region XQueryStrategy
        class XQueryStrategy<TRow> : IQueryStrategy<TRow>
        {

            List<Action<QueryStrategyExtensions.CommandBuilderState>> InternalCommandBuilder = new List<Action<QueryStrategyExtensions.CommandBuilderState>>();

            public List<Action<QueryStrategyExtensions.CommandBuilderState>> GetCommandBuilder()
            {
                return InternalCommandBuilder;
            }

            public Func<IQueryDescriptor> InternalGetDescriptor;

            public IQueryDescriptor GetDescriptor()
            {
                //  public static DataTable AsDataTable(IQueryStrategy Strategy)

                return InternalGetDescriptor();
            }
        }

        #endregion






        // group by . into .
        public static IQueryStrategy<TResult>
            Select
            <TSource, TKey, TResult>
            (
             this IQueryStrategyGroupingBuilder<TKey, TSource> GroupBy,
             Expression<Func<IQueryStrategyGrouping<TKey, TSource>, TResult>> keySelector)
        {
            // source = {TestSQLiteGroupBy.X.XQueryStrategy<System.Linq.IGrouping<TestSQLiteGroupBy.Data.GooStateEnum,TestSQLiteGroupBy.Data.Book1MiddleRow>>}
            // keySelector = {GroupByGoo => new Book1MiddleAsGroupByGooWithCountRow() {GooStateEnum = GroupByGoo.Key, Count = Convert(GroupByGoo.Count())}}

            // we are about to create a view just like we do in the join.
            // http://stackoverflow.com/questions/9287119/get-first-row-for-one-group


            //select GooStateEnum, count(*)
            //from `Book1.Middle`


            var GroupBy_asMemberExpression = GroupBy.keySelector.Body as MemberExpression;


            var asMemberInitExpression = keySelector.Body as MemberInitExpression;

            //-		Bindings	Count = 0x00000007	System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.MemberBinding> {System.Runtime.CompilerServices.TrueReadOnlyCollection<System.Linq.Expressions.MemberBinding>}

            //+		[0x00000000]	{GooStateEnum = GroupByGoo.Key}	System.Linq.Expressions.MemberBinding {System.Linq.Expressions.MemberAssignment}

            //+		[0x00000001]	{FirstTitle = GroupByGoo.First().Title}	System.Linq.Expressions.MemberBinding {System.Linq.Expressions.MemberAssignment}
            //+		[0x00000002]	{FirstKey = Convert(GroupByGoo.First())}	System.Linq.Expressions.MemberBinding {System.Linq.Expressions.MemberAssignment}
            //+		[0x00000003]	{LastKey = Convert(GroupByGoo.Last())}	System.Linq.Expressions.MemberBinding {System.Linq.Expressions.MemberAssignment}
            //+		[0x00000004]	{LastTitle = GroupByGoo.Last().Title}	System.Linq.Expressions.MemberBinding {System.Linq.Expressions.MemberAssignment}
            //+		[0x00000005]	{SumOfx = GroupByGoo.Sum(u => u.x)}	System.Linq.Expressions.MemberBinding {System.Linq.Expressions.MemberAssignment}
            //+		[0x00000006]	{Count = GroupByGoo.Count()}	System.Linq.Expressions.MemberBinding {System.Linq.Expressions.MemberAssignment}


            //        Y:\TestSQLiteGroupBy.ApplicationWebService\staging.java\web\java\TestSQLiteGroupBy\X___c__DisplayClass4_3___c__DisplayClass6.java:200: error: ';' expected
            //private static __MethodCallExpression _<Select>b__3_Isinst_001c(Object _001c)
            //                                       ^

            var that = new XQueryStrategy<TResult>
            {


                InternalGetDescriptor =
                    () =>
                    {
                        // inherit the connection/context from above
                        var StrategyDescriptor = GroupBy.source.GetDescriptor();

                        return StrategyDescriptor;
                    }
            };

            //    Caused by: java.lang.RuntimeException: { Message = Duplicate column name 'Key', StackTrace = java.sql.SQLException: Duplicate column name 'Key'
            //at com.google.cloud.sql.jdbc.internal.Exceptions.newSqlException(Exceptions.java:219)


            // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Shared\Data\Diagnostics\QueryStrategyExtensions.cs
            that.GetCommandBuilder().Add(
                 state =>
                 {
                     var s = QueryStrategyExtensions.AsCommandBuilder(GroupBy.source);

                     // http://www.xaprb.com/blog/2006/12/07/how-to-select-the-firstleastmax-row-per-group-in-sql/


                     // for the new view
                     // count is easy. 
                     // views should not care about keys, tags and timestamps?

                     // well the last seems to work
                     // not the first.


                     // Caused by: java.lang.RuntimeException: { Message = Every derived table must have its own alias,

                     state.SelectCommand =
                         //"select g.GooStateEnum as GooStateEnum,\n\t"
                         "select 0 as foo ";



                     ////+ "g.Count as Count,\n\t"


                     // + "g.`Key` as LastKey,\n\t"

                     //+ "g.x as Lastx,\n\t"
                     //+ "g.Title as LastTitle,\n\t"

                     //// aint working
                     //+ "gDescendingByKey.Key as FirstKey,\n\t"
                     //+ "gDescendingByKey.x as Firstx,\n\t"
                     //+ "gDescendingByKey.Title as FirstTitle,\n\t"

                     //+ "g.SumOfx as SumOfx,\n\t"

                     //+ "'' as Tag, 0 as Timestamp\n\t";




                     s.FromCommand += " as s";

                     // http://www.w3schools.com/sql/sql_func_last.asp
                     s.SelectCommand = "select 0 as foo";

                     //+ "s.x,\n\t"
                     // // specialname
                     //+ "s.`Key`,\n\t"
                     //+ "s.Title,\n\t"
                     // //+ "s.GooStateEnum,\n\t"
                     // + "sum(s.x) as SumOfx,\n\t";
                     //+ "13 as SumOfx, "
                     //+ "count(*) as Count";

                     asMemberInitExpression.Bindings.WithEachIndex(
                         (SourceBinding, index) =>
                         {
                             //{ index = 0, asMemberAssignment = MemberAssignment { Expression = MemberExpression { expression = ParameterExpression { type = TestSQLiteGroupBy.IQueryStrategyGrouping_2, name = GroupByGoo }, field =
                             //{ index = 0, asMemberExpression = MemberExpression { expression = ParameterExpression { type = TestSQLiteGroupBy.IQueryStrategyGrouping_2, name = GroupByGoo }, field = java.lang.Object get_Key() } }
                             //{ index = 0, Name = get_Key }
                             //{ index = 0, asMemberExpressionMethodCallExpression =  }
                             //{ index = 0, asUnaryExpression =  }

                             //{ index = 0, asMemberAssignment = GooStateEnum = GroupByGoo.Key }
                             //{ index = 0, asMemberExpression = GroupByGoo.Key }

                             //{ index = 1, asMemberAssignment = Count = GroupByGoo.Count() }
                             //{ index = 1, Method = Int64 Count(ScriptCoreLib.Shared.Data.Diagnostics.IQueryStrategy`1[TestSQLite

                             //{ index = 1, asMemberAssignment = MemberAssignment { Expression = MethodCallExpression { Object = , Method = long Count_060000af(ScriptCoreLib.Shared.Data.Diagnostics.IQueryStrategy_1) } } }
                             //{ index = 1, Method = long Count_060000af(ScriptCoreLib.Shared.Data.Diagnostics.IQueryStrategy_1) }
                             //{ index = 1, asMemberExpression =  }
                             //{ index = 1, asUnaryExpression =  }

                             //{ index = 2, asMemberAssignment = FirstTitle = GroupByGoo.First().Title }
                             //{ index = 2, asMemberExpression = GroupByGoo.First().Title }
                             //{ index = 3, asMemberAssignment = FirstKey = Convert(GroupByGoo.First()) }
                             //{ index = 3, asMemberExpression =  }
                             //{ index = 3, asUnaryExpression = Convert(GroupByGoo.First()) }
                             //{ index = 3, Method = TestSQLiteGroupBy.Data.Book1MiddleRow First[GooStateEnum,Book1MiddleRow](Test
                             //{ index = 4, asMemberAssignment = Firstx = GroupByGoo.First().x }
                             //{ index = 4, asMemberExpression = GroupByGoo.First().x }
                             //{ index = 5, asMemberAssignment = LastKey = Convert(GroupByGoo.Last()) }
                             //{ index = 5, asMemberExpression =  }
                             //{ index = 5, asUnaryExpression = Convert(GroupByGoo.Last()) }
                             //{ index = 5, Method = TestSQLiteGroupBy.Data.Book1MiddleRow Last[GooStateEnum,Book1MiddleRow](TestS
                             //{ index = 6, asMemberAssignment = LastTitle = GroupByGoo.Last().Title }
                             //{ index = 6, asMemberExpression = GroupByGoo.Last().Title }

                             //{ index = 7, asMemberAssignment = Lastx = GroupByGoo.Last().x }
                             //{ index = 7, asMemberExpression = GroupByGoo.Last().x }
                             //{ index = 7, Member = Double x, Name = x }
                             //{ index = 7, asMemberExpressionMethodCallExpression = GroupByGoo.Last() }
                             //{ index = 7, asMemberExpressionMethodCallExpression = GroupByGoo.Last(), Name = Last }

                             //{ index = 7, asMemberAssignment = MemberAssignment { Expression = MemberExpression { expression = MethodCallExpression { Object = , Method = java.lang.Object Last(TestSQLiteGroupBy.IQueryStrategyGrouping_2) }, field = double x }
                             //{ index = 7, asMemberExpression = MemberExpression { expression = MethodCallExpression { Object = , Method = java.lang.Object Last(TestSQLiteGroupBy.IQueryStrategyGrouping_2) }, field = double x } }
                             //{ index = 7, Member = double x, Name = x }
                             //{ index = 7, asMemberExpressionMethodCallExpression = MethodCallExpression { Object = , Method = java.lang.Object Last(TestSQLiteGroupBy.IQueryStrategyGrouping_2) } }


                             //{ index = 8, asMemberAssignment = SumOfx = GroupByGoo.Sum(u => u.x) }
                             //{ index = 8, Method = Double Sum[GooStateEnum,Book1MiddleRow](TestSQLiteGroupBy.IQueryStrategyGroup

                             //{ index = 8, asMemberAssignment = MemberAssignment { Expression = MethodCallExpression { Object = , Method = double Sum_06000128(TestSQLiteGroupBy.IQueryStrategyGrouping_2, ScriptCoreLi
                             //{ index = 8, Method = double Sum_06000128(TestSQLiteGroupBy.IQueryStrategyGrouping_2, ScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions.__Expression_1) }
                             //{ index = 8, asMemberExpression =  }
                             //{ index = 8, asUnaryExpression =  }


                             //{ index = 9, asMemberAssignment = Tag = GroupByGoo.Last().Tag }
                             //{ index = 9, asMemberExpression = GroupByGoo.Last().Tag }
                             //{ index = 10, asMemberAssignment = Timestamp = GroupByGoo.Last().Timestamp }
                             //{ index = 10, asMemberExpression = GroupByGoo.Last().Timestamp }


                             //                     Caused by: java.lang.RuntimeException: System.Diagnostics.Debugger.Break
                             //at ScriptCoreLibJava.BCLImplementation.System.Diagnostics.__Debugger.Break(__Debugger.java:31)
                             //at TestSQLiteGroupBy.X___c__DisplayClass4_3___c__DisplayClass6._Select_b__3(X___c__DisplayClass4_3___c__DisplayClass6.java:197)

                             // count and key
                             var asMemberAssignment = SourceBinding as MemberAssignment;
                             Console.WriteLine(new { index, asMemberAssignment });
                             if (asMemberAssignment != null)
                             {
                                 //                                 -		asMemberAssignment.Expression	{GroupByGoo.Count()}	System.Linq.Expressions.Expression {System.Linq.Expressions.MethodCallExpressionN}
                                 //+		Method	{Int64 Count(ScriptCoreLib.Shared.Data.Diagnostics.IQueryStrategy`1[TestSQLiteGroupBy.Data.Book1MiddleRow])}	System.Reflection.MethodInfo {System.Reflection.RuntimeMethodInfo}

                                 #region asMethodCallExpression
                                 var asMethodCallExpression = asMemberAssignment.Expression as MethodCallExpression;
                                 if (asMethodCallExpression != null)
                                 {
                                     Console.WriteLine(new { index, asMethodCallExpression.Method });

                                     #region count(*) special!
                                     if (asMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "Count")
                                     {

                                         state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "`";
                                         s.SelectCommand += ",\n\t count(*) as `" + asMemberAssignment.Member.Name + "`";

                                         return;
                                     }
                                     #endregion

                                     #region  sum( special!!
                                     if (asMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "Sum")
                                     {
                                         var arg1 = (asMethodCallExpression.Arguments[1] as UnaryExpression).Operand as LambdaExpression;
                                         if (arg1 != null)
                                         {
                                             var asMemberExpression = arg1.Body as MemberExpression;

                                             state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "`";
                                             s.SelectCommand += ",\n\t sum(s.`" + asMemberExpression.Member.Name + "`) as `" + asMemberAssignment.Member.Name + "`";
                                             return;
                                         }
                                     }
                                     #endregion

                                 }
                                 #endregion



                                 #region asMemberExpression
                                 {
                                     // m_getterMethod = {TestSQLiteGroupBy.Data.GooStateEnum get_Key()}

                                     var asMemberExpression = asMemberAssignment.Expression as MemberExpression;
                                     Console.WriteLine(new { index, asMemberExpression });
                                     if (asMemberExpression != null)
                                     {
                                         // +		Member	{TestSQLiteGroupBy.Data.GooStateEnum Key}	System.Reflection.MemberInfo {System.Reflection.RuntimePropertyInfo}

                                         // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Linq\Expressions\Expression.cs


                                         //{ index = 7, asMemberAssignment = MemberAssignment { Expression = MemberExpression { expression = MethodCallExpression { Object = , Method = java.lang.Object Last(TestSQLiteGroupBy.IQueryStrategyGrouping_2) }, field = double x }
                                         //{ index = 7, asMemberExpression = MemberExpression { expression = MethodCallExpression { Object = , Method = java.lang.Object Last(TestSQLiteGroupBy.IQueryStrategyGrouping_2) }, field = double x } }
                                         //{ index = 7, Member = double x, Name = x }
                                         //{ index = 7, asMemberExpressionMethodCallExpression = MethodCallExpression { Object = , Method = java.lang.Object Last(TestSQLiteGroupBy.IQueryStrategyGrouping_2) } }


                                         Console.WriteLine(new { index, asMemberExpression.Member, asMemberExpression.Member.Name });

                                         #region let z <- Grouping.Key
                                         var IsKey = asMemberExpression.Member.Name == "Key";

                                         // if not a property we may still have the getter in JVM
                                         IsKey |= asMemberExpression.Member.Name == "get_Key";

                                         if (IsKey)
                                         {
                                             // special!
                                             state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "`";

                                             s.SelectCommand += ",\n\t s.`" + GroupBy_asMemberExpression.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                             return;
                                         }
                                         #endregion

                                         // Method = {TestSQLiteGroupBy.Data.Book1MiddleRow First[GooStateEnum,Book1MiddleRow](TestSQLiteGroupBy.IQueryStrategyGrouping`2[TestSQLiteGroupBy.Data.GooStateEnum,TestSQLiteGroupBy.Data.Book1MiddleRow])}
                                         var asMemberExpressionMethodCallExpression = asMemberExpression.Expression as MethodCallExpression;
                                         Console.WriteLine(new { index, asMemberExpressionMethodCallExpression });
                                         if (asMemberExpressionMethodCallExpression != null)
                                         {
                                             Console.WriteLine(new { index, asMemberExpressionMethodCallExpression, asMemberExpressionMethodCallExpression.Method.Name });

                                             // special!
                                             if (asMemberExpressionMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "First")
                                             {
                                                 state.SelectCommand += ",\n\t gDescendingByKey.`" + asMemberAssignment.Member.Name + "`";
                                                 s.SelectCommand += ",\n\t s.`" + asMemberExpression.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                                 return;
                                             }

                                             if (asMemberExpressionMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "Last")
                                             {
                                                 state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "`";
                                                 s.SelectCommand += ",\n\t s.`" + asMemberExpression.Member.Name + "` as `" + asMemberAssignment.Member.Name + "`";
                                                 return;
                                             }
                                         }
                                     }
                                 }
                                 #endregion

                                 #region  asMemberAssignment.Expression = {Convert(GroupByGoo.First())}
                                 var asUnaryExpression = asMemberAssignment.Expression as UnaryExpression;

                                 Console.WriteLine(new { index, asUnaryExpression });

                                 if (asUnaryExpression != null)
                                 {
                                     var asMemberExpressionMethodCallExpression = asUnaryExpression.Operand as MethodCallExpression;
                                     if (asMemberExpressionMethodCallExpression != null)
                                     {
                                         Console.WriteLine(new { index, asMemberExpressionMethodCallExpression.Method });
                                         // special! op_Implicit
                                         if (asMemberExpressionMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "First")
                                         {
                                             state.SelectCommand += ",\n\t gDescendingByKey.`" + asMemberAssignment.Member.Name + "`";
                                             s.SelectCommand += ",\n\t s.`Key` as `" + asMemberAssignment.Member.Name + "`";
                                             return;
                                         }

                                         if (asMemberExpressionMethodCallExpression.Method.Name.TakeUntilIfAny("_") == "Last")
                                         {
                                             state.SelectCommand += ",\n\t g.`" + asMemberAssignment.Member.Name + "`";
                                             s.SelectCommand += ",\n\t s.`Key` as `" + asMemberAssignment.Member.Name + "`";
                                             return;
                                         }
                                     }
                                 }
                                 #endregion



                             }

                             Debugger.Break();
                         }
                     );


                     // how do we get the first and the last in the same query??


                     //+ "3 as Count";

                     // error: { Message = no such column: g.GooStateEnum, ex = System.Data.SQLite.SQLiteSyntaxException (0x80004005): no such column: g.GooStateEnum

                     // http://stackoverflow.com/questions/27983/sql-group-by-with-an-order-by
                     // MySQL prior to version 5 did not allow aggregate functions in ORDER BY clauses.


                     //s.OrderByCommand = "order by GooStateEnum desc";

                     // what about distinct? 
                     // we cannot reorder the table by the grouping item
                     // we would have to rely on PK Key? either assume Key was generated by AssetsLibrary
                     // or crash or inspect the table by explain

                     //s.GroupByCommand = "group by GooStateEnum";
                     s.GroupByCommand = "group by `" + GroupBy_asMemberExpression.Member.Name + "`";

                     // http://www.afterhoursprogramming.com/tutorial/SQL/ORDER-BY-and-GROUP-BY/
                     // CANNOT limit nor order if we are about to group.

                     //s.LimitCommand = "limit 1";


                     //select g.GooStateEnum as GooStateEnum, g.Count as Count
                     //from (
                     //        select GooStateEnum, count(*) as Count
                     //        from `Book1.Middle`
                     //         where `FooStateEnum` = @arg0 and `Ratio` > @arg1 and `Ratio` < @arg2


                     //        group by GooStateEnum
                     //        ) as g


                     // how can we pass arguments to the flattened where?\
                     // g seems to be last inserted?



                     //                 var FromCommand =
                     //"from (\n\t"
                     //    + xouter_SelectAll.ToString().Replace("\n", "\n\t")
                     //    + ") as " + xouter_Paramerer_Name.Replace("<>", "__") + " "

                     //    + "\ninner join (\n\t"
                     //    + xinner_SelectAll.ToString().Replace("\n", "\n\t")
                     //    + ") as " + xinner_Paramerer.Name.Replace("<>", "__");

                     state.FromCommand =
                          "from (\n\t"
                            + s.ToString().Replace("\n", "\n\t")
                            + "\n) as g ";

                     // http://msdn.microsoft.com/en-us/library/vstudio/bb386996(v=vs.100).aspx


                     // hack it. no longer useable later
                     // http://help.sap.com/abapdocu_702/en/abaporderby_clause.htm#!ABAP_ALTERNATIVE_1@1@
                     //  ORDER BY { {PRIMARY KEY}

                     s.FromCommand = "from (select * " + s.FromCommand + " order by `Key` desc) as s";
                     //s.FromCommand = "from (select * " + s.FromCommand + " order by PRIMARY KEY desc)";

                     state.FromCommand +=
                        "inner join (\n\t"
                           + s.ToString().Replace("\n", "\n\t")
                            + "\n) as gDescendingByKey";

                     //state.FromCommand += " on g.GooStateEnum = gDescendingByKey.GooStateEnum";
                     state.FromCommand += " on g.`" + GroupBy_asMemberExpression.Member.Name + "` = gDescendingByKey.`" + GroupBy_asMemberExpression.Member.Name + "`";


                     //select g.GooStateEnum as GooStateEnum, g.Key as LastKey, g.x as Lastx, g.Title as LastTitle, gDescendingByKey.Key as FirstKey, gDescendingByKey.x as Firstx, gDescendingByKey.Title as FirstTitle, g.Count as Count, '' as Tag, 0 as Timestamp
                     //from (
                     //        select x,Key, Title, GooStateEnum, count(*) as Count
                     //        from `Book1.Middle`
                     //         where `FooStateEnum` = @arg0 and `Ratio` = @arg1


                     //        group by GooStateEnum

                     //) as g inner join (
                     //        select x,Key, Title, GooStateEnum, count(*) as Count
                     //        from (select * from `Book1.Middle` order by Key desc)
                     //         where `FooStateEnum` = @arg0 and `Ratio` = @arg1


                     //        group by GooStateEnum

                     //) as gDescendingByKey on g.GooStateEnum = gDescendingByKey.GooStateEnum




                     ////state.FromCommand += ", (\n\t"
                     ////       + s.ToString().Replace("\n", "\n\t")
                     ////       + "\n) as gFirst ";



                     // copy em?
                     state.ApplyParameter.AddRange(s.ApplyParameter);

                 }
             );


            return that;
        }

        public static IQueryStrategyGroupingBuilder<TKey, TSource>
            GroupBy
            <TSource, TKey>
            (
         this IQueryStrategy<TSource> source,
         Expression<Func<TSource, TKey>> keySelector)
        {
            return new XQueryStrategyGroupingBuilder<TKey, TSource> { source = source, keySelector = keySelector };
        }
    }



    public class XGrouping
    {
        public GooStateEnum Key;
        public IEnumerable<Book1MiddleRow> Items;
    }
}
