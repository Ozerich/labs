.model small
.stack 100h

.code
	old_handler dd ?
	my_string db 'Its works!!!$'
	test_string db 'hui$'
	counter dw 0

new_handler proc
	push ds
	push si
	push es
	push di
	push dx
	push cx
	push bx
	push ax

	xor ax, ax
	in al, 60h
	cmp al, 1Eh
	je change_key
	cmp al, 18h
	je change_key
	cmp al, 17h
	je change_key
	cmp al, 16h
	je change_key
	jmp no_change

change_key:
	inc counter
	mov ax, counter
	mov bx, 2
	xor dx, dx
	div bx
	cmp dx, 0
	je exit

no_change:
	pop ax
	pop bx
	pop cx
	pop dx
	pop di
	pop es
	pop si
	pop ds

	jmp dword ptr cs:[old_handler]
	xor ax, ax
	mov al, 20h
	out 20h, al
	jmp exit
exit:
	xor ax, ax
	mov al, 20h
	out 20h, al

	pop ax
	pop bx
	pop cx
	pop dx
	pop di
	pop es
	pop si
	pop ds
iret

new_handler endp
new_end:

start:
	mov ah, 35h
	mov al, 09h
	int 21h

	mov word ptr old_handler, bx
	mov word ptr old_handler + 2, es

	mov ax, @code
	mov ds, ax
	mov ax, 2509h
	mov dx, offset new_handler
	int 21h

	mov dx, (new_end - start + 10fh) / 16
	mov ah, 31h
	int 21h
end start

