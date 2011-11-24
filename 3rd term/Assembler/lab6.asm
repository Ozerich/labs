.model small
.stack 1000h

.data 
	input_n db 'N: ', '$'
	input_m db 'M: ', '$'
	input_matrix1 db 'Matrix 1: ', 10, 13, '$'
	input_matrix2 db 'Matrix 2: ', 10, 13, '$'
	output_matrix3 db 'Matrix 3: ', 10, 13, '$'
	
	n dw ?
	m dw ?
	
	matrix1 dw 255 dup (?)
	matrix2 dw 255 dup (?)
	matrix3 dw 255 dup (?)
	
	cx_temp dw ?
	
	int_str db 8 dup(?)
	
.code
.486

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
		mov [int_str+BX], DL
		inc BX
		cmp AX, 0
		je print_str
		jmp process_str
	print_str:
		dec BX
		mov AX, 0
		mov AL, [int_str+BX]
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

write_newline proc
	mov AX, 0Ah
	push AX
	call write_char
	mov AX, 0Dh
	push AX
	call write_char
	ret
write_newline endp

read_size proc
	mov AH, 9h
	mov DX, offset input_n
	int 21h
	call read_num
	pop AX
	mov [n], AX
	
	mov AH, 9h
	mov DX, offset input_m
	int 21h
	call read_num
	pop AX
	mov [m], AX

	ret
read_size endp

read_matrix1 proc
	mov AH, 9h
	mov DX, offset input_matrix1
	int 21h
	mov CX, [n]
	mov SI, 0
	out_cycle:
		push CX
		mov CX, [m]
		in_cycle:
			mov [cx_temp], CX
			call read_num
			mov CX, [cx_temp]
			pop AX
			mov matrix1[si], AX
			add SI, 2
			loop in_cycle
		pop CX
		loop out_cycle
	ret
read_matrix1 endp

read_matrix2 proc
	mov AH, 9h
	mov DX, offset input_matrix2
	int 21h
	mov CX, [n]
	mov SI, 0
	out_cycle2:
		push CX
		mov CX, [m]
		in_cycle2:
			mov [cx_temp], CX
			call read_num
			mov CX, [cx_temp]
			pop AX
			mov matrix2[si], AX
			add SI, 2
			loop in_cycle2
		pop CX
		loop out_cycle2
	ret
read_matrix2 endp

make_matrix3 proc
	mov CX, [n]
	mov SI, 0
	out_cycle3:
		push CX
		mov CX, [m]
		in_cycle3:
			mov [cx_temp], CX
			mov AX, matrix1[SI]
			mov BX, matrix2[SI]
			cmp AX, BX
			jbe a_less
			jmp a_good
			a_less:
				mov AX, BX
			a_good:
				mov matrix3[SI], AX
				add SI, 2
			loop in_cycle3
		pop CX
		loop out_cycle3
			
	ret
make_matrix3 endp

write_matrix3 proc
	mov AH, 9h
	mov DX, offset output_matrix3
	int 21h
	mov CX, [n]
	mov SI, 0
	out_cycle4:
		push CX
		mov CX, [m]
		in_cycle4:
			mov [cx_temp], CX
			mov AX, matrix3[SI]
			push AX
			call write_int
			mov CX, [cx_temp]
			mov AX, 20h
			push AX
			call write_char
			add SI, 2
			loop in_cycle4
		pop CX
		call write_newline
		loop out_cycle4
	ret
write_matrix3 endp

main:
	mov ax, @data
	mov ds, ax

	call read_size
	call read_matrix1
	call read_matrix2
	call make_matrix3
	call write_matrix3
	
	mov AH, 4ch
	int 21h	
end main