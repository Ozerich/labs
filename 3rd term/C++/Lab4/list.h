#ifndef LIST_H_INCLUDED
#define LIST_H_INCLUDED

typedef void (*IteratorFunc)(void*);

template<class T>
struct ListItem{
public:
    T value;
    ListItem<T> *next;
    ListItem(T val)
    {
        value = val;
    }
};

template<class T>
class ListIterator
{
private:
    ListItem<T> *listItem;
public:
    ListIterator<T>(ListItem<T> *_listItem)
    {
        listItem = _listItem;
    }

    void next()
    {
        listItem = listItem->next();
    }

    T get()
    {
        return listItem->value;
    }
};


template<class T>
class List{
private:
    ListItem<T> *root;
public:
    List<T>()
    {
        root = 0;
    }

    ~List()
    {
        ListItem<T> *nxt;
        while(root)
        {
            nxt = root->next;
            delete root;
            root = nxt;
        }
    }

    ListIterator<T> *iter()
    {
        return new ListIterator<T>(root);
    }

    ListItem<T> *begin()
    {
        return root;
    }

    T get(int idx)
    {
        ListItem<T> *cur = root;
        for(int i = 0; i < idx; i++)
            cur = cur->next();
        return cur->value;
    }

    int count()
    {
        ListItem<T> *cur = root;
        int res = 0;
        while(cur != 0)
        {
            cur = cur->next();
            res++;
        }
    }

    int find(T value)
    {
        ListItem<T> *cur = root;
        int index = 0;
        while(true)
        {
            cur = cur->next();
            if(cur->value == value)
                return index;
            index++;
        }
        return -1;
    }

    void ForEach(IteratorFunc func)
    {
        ListItem<T> *cur = root;
        while(cur)
        {
            func(cur);
            cur = cur->next;
        }
    }

    void Remove(int index)
    {
        if(index < 0)
            return;
        else if(index == 0)
        {
            ListItem<T> *prev = root;
            root = root->next;
            delete prev;
        }
        else
        {
            ListItem<T> *prev = root;
            ListItem<T> *cur = root->next();
            for(int i = 0; i < index - 1; i++)
            {
                prev = cur;
                cur = prev->next();
            }
            prev->next = cur->next();
            delete cur;
        }
    }

    void RemoveByVal(T val)
    {
        Remove(find(val));
    }

    void Add(int val)
    {
        ListItem<T> *newElem = new ListItem<T>(val);
        newElem->next = root;
        root = newElem;
    }
};


#endif // LIST_H_INCLUDED
