.model small
.stack 100h
.data
	a dw ?
	b dw ?
	n dw ?
	str db 8 dup(?)
.code

main:
mov AX, @data
mov DS, AX

call read_num
pop a
call read_num
pop b

mov AX, a
mov CX, 1
mov n, CX
cmp AX, b
jbe a_less_b
cycl:
	mov AX, BX
	mov BX, b
	mul BX
	cmp AX, a
	jae end_prog
	mov BX, AX
	mov AX, n
	inc AX
	mov n, AX
	jmp cycl
a_less_b:
	mov n, 0
	jmp end_prog
		
end_prog:
	push n
	call write_int
	mov AX, 0
	mov AH, 4ch
	int 21h

write_char proc
	pop BP
	pop DX
	mov AH, 02h
	int 21h
	push BP
	ret
write_char endp

write_int proc
	pop BP
	pop AX
	mov CX, 10
	xor BX, BX
	process_str:
		mov DX, 0
		div CX
		mov [str+BX], DL
		inc BX
		cmp AX, 0
		je print_str
		jmp process_str
	print_str:
		dec BX
		mov AX, 0
		mov AL, [str+BX]
		add AX, 30h
		push BP
		push AX
		call write_char
		pop BP
		cmp BX, 0
		jne print_str	
	push BP
	ret
write_int endp

read_num proc
	pop BP
	xor CX, CX
	read_char:
		mov AH, 01h
		int 21h
		cmp AL, 30h
		jl end_read
		cmp AL, 39h
		jg end_read
		sub AL, 30h
		mov BL, AL
		mov AX, CX
		mov CX, 10
		mul CX
		mov CX, AX
		add CX, BX
		jmp read_char
	end_read:
		push CX
		push BP
		ret
read_num endp
end main
