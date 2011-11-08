.model small
.stack 100h
.data
	inputstr_msg db 'Input string: ', 10, 13, '$'
	inputint_msg db 'Input number: ', 10, 13, '$'
	result_msg db 'Result: ', 10, 13, '$'
	input_str db 256 dup('?')
	res_str db 10 dup('?')
	num dw ?
	strlen dw ?
.code

read_str proc
	pop BP
	xor BX, BX
	read_schar:
		mov AH, 01h
		int 21h
		cmp AL, 13
		je end_sread
		cmp BX, 255
		jnl end_sread
		mov [input_str + BX], AL
		inc BX
		jmp read_schar
	end_sread:
		mov [input_str + BX], '$'
		push BX
		push BP
		ret
read_str endp

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

delete proc
	pop BP
	xor BX, BX
	xor CX, CX
	next_char:
		inc BX
		cmp BX, strlen
		jg end_delete
		
		mov AX, BX
		mov DH, BYTE PTR[num]
		div DH
		cmp AH, 0
		je need_delete		
		
		push BX
		dec BX
		mov AL, [input_str + BX]
		sub BX, CX
		mov [res_str + BX], AL
		pop BX
		
		jmp next_char
	need_delete:
		inc CX
		jmp next_char
	end_delete:
		sub BX, CX
		dec BX
		mov [res_str + BX], '$'
		push BP
		ret
delete endp

main:
	mov AX, @data
	mov DS, AX
	
	mov AH, 9h
	mov DX, offset inputstr_msg
	int 21h
	call read_str
	pop strlen
	
	mov AH, 9h
	mov DX, offset inputint_msg
	int 21h
	call read_num
	pop num
	
	call delete
	
	mov AH, 9h
	mov DX, offset result_msg
	int 21h
	mov DX, offset res_str
	int 21h
	
	mov AH, 4ch
	int 21h	
end main
