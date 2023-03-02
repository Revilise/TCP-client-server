using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class ClientsList : IList<ClientHandler>
    {
        public ClientsList() { }

        private List<ClientHandler> _collection = new List<ClientHandler>();
        public ClientHandler this[int index] {
            get => _collection[index];
            set => _collection[index] = value; 
        }

        public int Count => _collection.Count;

        public bool IsReadOnly => false;

        public void Add(ClientHandler item)
        {
            _collection.Add(item);
            Thread clientThread = new Thread(new ThreadStart(item.RunClient));
            clientThread.Start();
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public bool Contains(ClientHandler item)
        {
            return _collection.Contains(item);
        }

        public void CopyTo(ClientHandler[] array, int arrayIndex)
        {
            _collection.CopyTo(array, arrayIndex);
        }

        public IEnumerator<ClientHandler> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public int IndexOf(ClientHandler item)
        {
            return _collection.IndexOf(item);
        }

        public void Insert(int index, ClientHandler item)
        {
            _collection.Insert(index, item);
        }

        public bool Remove(ClientHandler item)
        {
            return _collection.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _collection.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }
    }
}
