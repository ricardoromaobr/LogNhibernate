using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Log.Domain.Mapping;
using Log.Domain.Model;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Type;
using System.Collections;
using Grace.DependencyInjection;

namespace LogNhibernateTeste
{
    class Program
    {
        static void Main(string[] args)
        {

            DependencyInjectionContainer container = new DependencyInjectionContainer();
            container.Configure(c => c.Export<Servico>().As<IServico>());

            var sessionFactory = CreateSessionFactory();
            var interceptor = new LogInterceptor();

            interceptor.Container = container;

            var session = sessionFactory.OpenSession(interceptor);
            interceptor.Session = session;


            using (var tran = session.BeginTransaction())
            {
                var p1 = new PessoaFisica
                {
                    Nome = "Ricardo Romão Soares",
                    Cpf = "1212121"

                };

                var p2 = new PessoaFisica
                {
                    Nome = "Rogéria Silva de Abreu Soares",
                    Cpf = "2222"
                };


                session.Save(p2);
                session.Save(p1);

                tran.Commit();
            }

            //session.Flush();
            session.Close();
            session.Dispose();

            session = sessionFactory.OpenSession(interceptor);
            interceptor.Session = session;

            using (var tran = session.BeginTransaction())
            {
                var p3 = session.Query<Pessoa>().First();
                p3.Nome = "Ricardo";
                session.SaveOrUpdate(p3);
                tran.Commit();
            }


            //session.Flush();
            session.Close();
            session.Dispose();

            session = sessionFactory.OpenSession(interceptor);
            interceptor.Session = session;

            using (var tran = session.BeginTransaction())
            {
                var p3 = session.Query<Pessoa>().First();
                session.Delete(p3);
                tran.Commit();
            }
        }

        static string dbfile = "firstProject.db";
        static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(
                    SQLiteConfiguration.Standard
                            .UsingFile(dbfile))
                .Mappings(m =>
                        m.FluentMappings.AddFromAssemblyOf<PessoaMap>())
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            // delete the existing db on each run
            if (File.Exists(dbfile))
                File.Delete(dbfile);

            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            new SchemaExport(config)
              .Create(false, true);
        }
    }


    class LogInterceptor : EmptyInterceptor
    {

        public DependencyInjectionContainer Container { get; set; }
        public ISession Session { get; set; }
        public override bool OnSave(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            return base.OnSave(entity, id, state, propertyNames, types);
        }

        public override bool OnFlushDirty(object entity, object id, object[] currentState, object[] previousState, string[] propertyNames, IType[] types)
        {
            return base.OnFlushDirty(entity, id, currentState, previousState, propertyNames, types);
        }

        public override void PostFlush(ICollection entities)
        {
            base.PostFlush(entities);
        }

        public override void PreFlush(ICollection entitites)
        {
            base.PreFlush(entitites);
        }

        public override void OnDelete(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            base.OnDelete(entity, id, state, propertyNames, types);
        }

        public override object Instantiate(string clazz, EntityMode entityMode, object id)
        {
            if (entityMode == EntityMode.Poco)
            {
                //var type = Type.GetType(clazz + ",Log.Domain");
                var type = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => !a.IsDynamic)
                    .SelectMany(a => a.GetTypes())
                    .FirstOrDefault(t => t.FullName.Equals(clazz));

                if (type != null)
                {
                    var instance = Container.Locate(type);
                    var md = Session.SessionFactory.GetClassMetadata(clazz);
                    md.SetIdentifier(instance, id, entityMode);
                    return instance;
                }
            }
            return base.Instantiate(clazz, entityMode, id);
        }


        /*
        public class DependencyInjectionInterceptor : EmptyInterceptor
        {
            private readonly IUnityContainer _container;
            private ISession _session;

            public DependencyInjectionInterceptor(IUnityContainer container)
            {
                _container = container;
            }

            public void SetSession(ISession session)
            {
                _session = session;
            }

            public override object Instantiate(string clazz, EntityMode entityMode, object id)
            {
                if (entityMode == EntityMode.Poco)
                {
                    var type = Type.GetType(clazz);
                    if (type != null)
                    {
                        var instance = _container.Resolve(type);
                        var md = _session.SessionFactory.GetClassMetadata(clazz);
                        md.SetIdentifier(instance, id, entityMode);
                        return instance;
                    }
                }
                return base.Instantiate(clazz, entityMode, id);
            }
        }

    */
    }
}
