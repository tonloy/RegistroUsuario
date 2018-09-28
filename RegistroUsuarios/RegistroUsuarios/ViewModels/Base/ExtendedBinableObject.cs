using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using Xamarin.Forms;

namespace RegistroUsuarios.ViewModels.Base
{
    public abstract class ExtendedBinableObject : BindableObject
    {
        public void RaisePropertyChanged<T>(Expression<Func<T>> property) {
            var name = GetMemberInfo(property).Name;
            OnPropertyChanged(name);
        }

        private MemberInfo GetMemberInfo<T>(Expression<Func<T>> expression)
        {
            MemberExpression operando;
            LambdaExpression lambda = (LambdaExpression)expression;

            if (lambda.Body as UnaryExpression != null)
            {
                UnaryExpression body = (UnaryExpression)lambda.Body;
                operando = (MemberExpression)body.Operand;
            }
            else {
                operando = (MemberExpression)lambda.Body;
            }
            return operando.Member;
        }
    }
}
