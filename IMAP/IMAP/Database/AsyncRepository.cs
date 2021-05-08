using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace IMAP.Database
{
    //Класс для взаимодействия с базой данных

    //TODO сделать интерфейс для взаиодействия с базой данных

    public class AsyncRepository
    {
        protected SQLiteAsyncConnection database;
        public AsyncRepository(string databasePath)
        {
            database = new SQLiteAsyncConnection(databasePath);
        }

        public async Task CreateTable<T>() where T : new()
        {
            await database.CreateTableAsync<T>();
        }

        public async Task<List<T>> GetItemsAsync<T>() where T : new()
        {
            return await database.Table<T>().ToListAsync();
        }

        public async Task<T> GetItemAsync<T>(int id) where T : new()
        {
            return await database.GetAsync<T>(id);
        }

        public async Task<int> SaveItemAsync<T>(T item) where T : new()//TODO необходима реализация в базовом классе AsyncRepository
        {
            //Проверка на наличие записи в базе по id
            //Если есть, то обновляем запись, если нет, то записываем
            await database.UpdateAsync(item);

            if (await RowExists<T>(Guid.Parse(item.GetType().GetProperty("Id").GetValue(item, null).ToString())) == false)
            {
                return await database.InsertAsync(item);
            }
            else
            {
                return await database.UpdateAsync(item);
            }
        }

        public async Task<int> DeleteItemAsync<T>(T item) //Удаление записи в таблице
        {
            return await database.DeleteAsync(item);
        }

        public async Task<bool> RowExists<T>(Guid id) where T : new()//Проверка на наличие записи в таблице
        {
            //кол-во соотвествий в базе
            var Items = await database.QueryAsync<T>($"Select * from {typeof(T).Name} where _id=?", id.ToString());

            if (Items.Count == 0) //Если записей нет
            {
                return false;
            }
            else //Если запись есть
            {
                return true;
            }
        }
    }
}

