using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;

namespace ByteBank.Infra.Bindings
{
    public class ActionBindingInfo
    {
        public MethodInfo Method { get; private set; }
        public NameValueCollection Parameters { get; private set; }

        public ActionBindingInfo(MethodInfo method, NameValueCollection parameters)
        {
            Method = method ?? throw new ArgumentNullException(nameof(method));
            Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
        }

        public ActionBindingInfo(MethodInfo method, Dictionary<string, string> nameValuePairs)
        {
            Method = method ?? throw new ArgumentNullException(nameof(method));

            Parameters = new NameValueCollection();

            if(nameValuePairs is null) throw new ArgumentNullException(nameof(nameValuePairs));

            foreach (var nameValuePair in nameValuePairs)
                Parameters.Add(nameValuePair.Key, nameValuePair.Value);
        }

        public object Invoke(object controller)
        {
            if(Parameters.Count == 0)
                return Method.Invoke(controller, new object[0]);

            var parametersMethodInfo = Method.GetParameters();
            var parametersMethodInvoke = new object[parametersMethodInfo.Length];
            for(int i = 0; i < parametersMethodInfo.Length; i++)
            {
                var parameterName = parametersMethodInfo[i].Name;
                parametersMethodInvoke[i] = Convert.ChangeType(Parameters.Get(parameterName), parametersMethodInfo[i].ParameterType);
                Parameters.Remove(parameterName);
            }

            return Method.Invoke(controller, parametersMethodInvoke);
        }
    }
}
