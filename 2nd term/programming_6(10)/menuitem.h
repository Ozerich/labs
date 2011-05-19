#ifndef MENUITEM_H
#define MENUITEM_H

class MenuItem
{
	public int id;
	public char* name;
	public void (*onClick)(void);
}

#endif MENUITEM_H