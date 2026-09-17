// using System;
// using System.Collections.Generic;
// using UnityEngine;
//
// namespace CodeBase.Infrastructure.Common.Pool
// {
//     public abstract class Pool<T> where T : class, IPoolable
//     {
//         public bool AutoExpand { get; set; }
//         public Transform Container { get; }
//         
//         protected List<T> _pool;
//
//         public Pool(int count, Transform container)
//         {
//             Container = container;
//             
//             CreatePool(count);
//         }
//
//         private void CreatePool(int count)
//         {
//             _pool = new List<T>();
//
//             for (int i = 0; i < count; i++)
//             {
//                 CreateObject();
//             }
//         }
//
//         public virtual T CreateObject(bool isActiveByDefault = false)
//             => throw new NotSupportedException($"{GetType().Name} does not support CreateObject()");
//
//         public virtual T CreateObject<T1>(T1 arg1, bool isActiveByDefault = false)
//             => throw new NotSupportedException($"{GetType().Name} does not support CreateObject<T1>()");
//
//         public virtual T CreateObject<T1, T2>(T1 arg1, T2 arg2, bool isActiveByDefault = false)
//             => throw new NotSupportedException($"{GetType().Name} does not support CreateObject<T1,T2>()");
//
//         public virtual T CreateObject<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3, bool isActiveByDefault = false)
//             => throw new NotSupportedException($"{GetType().Name} does not support CreateObject<T1,T2,T3>()");
//
//         public bool HasFreeElement(out T element)
//         {
//             foreach (T mono in _pool)
//             {
//                 if (!mono.IsActiveInHierarchy)
//                 {
//                     element = mono;
//                     mono.SetActive(true);
//                     return true;
//                 }
//             }
//             
//             element = null;
//             return false;
//         }
//
//         public T GetFreeElement()
//         {
//             if (HasFreeElement(out T element))
//             {
//                 return element;
//             }
//
//             if (AutoExpand)
//             {
//                 CreateObject(true);
//             }
//             
//             throw new Exception($"There is no free elements in the pool {typeof(T)}");
//         }
//     }
//     public class Pool<in T1, T> where T : class, IPoolable
//     {
//         public bool AutoExpand { get; set; }
//         public Transform Container { get; }
//         
//         protected List<T> _pool;
//
//         public Pool(int count, Transform container)
//         {
//             Container = container;
//             
//             CreatePool(count);
//         }
//
//         private void CreatePool(int count)
//         {
//             _pool = new List<T>();
//
//             for (int i = 0; i < count; i++)
//             {
//                 CreateObject();
//             }
//         }
//
//         public virtual T CreateObject(T1 arg1, bool isActiveByDefault = false)
//             => throw new NotSupportedException($"{GetType().Name} does not support CreateObject<T1>()");
//
//         /*
//         public virtual T CreateObject<T1, T2>(T1 arg1, T2 arg2, bool isActiveByDefault = false)
//             => throw new NotSupportedException($"{GetType().Name} does not support CreateObject<T1,T2>()");
//
//         public virtual T CreateObject<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3, bool isActiveByDefault = false)
//             => throw new NotSupportedException($"{GetType().Name} does not support CreateObject<T1,T2,T3>()");*/
//
//         public bool HasFreeElement(out T element)
//         {
//             foreach (T mono in _pool)
//             {
//                 if (!mono.IsActiveInHierarchy)
//                 {
//                     element = mono;
//                     mono.SetActive(true);
//                     return true;
//                 }
//             }
//             
//             element = null;
//             return false;
//         }
//
//         public T GetFreeElement()
//         {
//             if (HasFreeElement(out T element))
//             {
//                 return element;
//             }
//
//             if (AutoExpand)
//             {
//                 CreateObject(true);
//             }
//             
//             throw new Exception($"There is no free elements in the pool {typeof(T)}");
//         }
//     }
//
//     /*public interface IPool
//     {
//     }
//
//     public interface IPool<T, in T1> : IPool where T : class, IPoolable 
//     {
//         T CreateObject(T1 arg1, bool isActiveByDefault = false);
//     }
//     public interface IPool<T, in T1, in T2> where T : class, IPoolable
//     {
//         T CreateObject(T1 arg1, T2 arg2, bool isActiveByDefault = false);
//     }
//     public interface IPool<T, in T1, in T2, in T3> where T : class, IPoolable
//     {
//         T CreateObject(T1 arg1, T2 arg2, T3 arg3, bool isActiveByDefault = false);
//     }*/
// }