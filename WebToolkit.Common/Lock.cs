﻿using System;
using System.ComponentModel;
using JetBrains.Annotations;
using WebToolkit.Contracts;

namespace WebToolkit.Common
{
    /// <summary>
    /// Represents a Lock that supports multi threading
    /// </summary>
    public sealed class Lock : ILock
    {
        public Action DoWork { get; }
        public void Run()
        {
            lock(_lockObject)
                DoWork();
        }

        /// <summary>
        /// Creates a Lock that carries out a single task in multi-threading supported context
        /// </summary>
        /// <param name="doWork"></param>
        /// <returns>Returns an instance of a Lock that supports multi-threading</returns>
        public static Lock Create(Action doWork)
        {
            return new Lock(doWork);
        }

        /// <summary>
        /// Creates a Lock that carries out a single task that returns a value in multi-threading supported context
        /// </summary>
        /// <param name="doWork"></param>
        /// <returns>Returns result processed by action</returns>
        public static Lock<TResult> Create<TResult>(TResult initialValue, Func<TResult, TResult> doWork)
        {
            return Lock<TResult>.Create(initialValue, doWork);
        }

        private readonly object _lockObject = new object();
        private Lock(Action doWork)
        {
            DoWork = doWork;
        }
    }

    public sealed class Lock<TResult> : ILock<TResult>
    {
        private readonly object _lockObject = new object();
        public static Lock<TResult> Create(TResult initialValue, Func<TResult, TResult> doWork)
        {
            return new Lock<TResult>(initialValue, doWork);
        }

        /// <summary>
        /// Value generated by DoWork
        /// </summary>
        public TResult Value { get; private set; }

        Action ILock.DoWork => throw new NotSupportedException();

        
        /// <summary>
        /// Creates a Lock that carries out a single task that returns a value in multi threading supported context
        /// </summary>
        /// <param name="doWork"></param>
        /// <returns>Returns result processed by action</returns>
        public Func<TResult, TResult> DoWork { get; }

        /// <summary>
        /// Executes lock
        /// </summary>
        public void Run()
        {
            lock (_lockObject)
            {
                var oldValue = Value;
                Value = DoWork(Value);
                OnPropertyChanged(nameof(Value));
            }
        }

        private Lock(TResult initialValue, Func<TResult, TResult> doWork)
        {
            Value = initialValue;
            DoWork = doWork;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}