.model small
.stack 100h
.data
    str db 8 dup(0)
    inputstr_msg db 'Input date: ', 10, 13, '$'
    input_str db 256 dup(0)
    strlen dw ?

    error1 db 'Incorrect length.', 10, 13, '$'
    error2 db 'Incorrect format.', 10, 13, '$'
    error3 db 'Hours not in [00-23].', 10, 13, '$'
    error4 db 'Minutes not in [00-59].', 10, 13, '$'
    error5 db 'Seconds not in [00-59].', 10, 13, '$'
    good_str db 'Good input. Answer: ', '$'
    
    hours dw ?
    minutes dw ?
    seconds dw ?

    result dw ?
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
	after_minus:
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


main:

mov AX, @data
mov DS, AX

mov AH, 9h
mov DX, offset inputstr_msg
int 21h

call read_str
pop AX

cmp AX, 8
jne bad_len


mov AL, [input_str]
mov BL, [input_str + 1]

cmp AL, 30h
jl bad_format
cmp AL, 39h
jg bad_format

cmp BL, 30h
jl bad_format
cmp BL, 39h
jg bad_format

sub AX, 30h
sub BX, 30h
mov CL, 10
mul CL
add AX, BX
mov [hours], AX

cmp [input_str + 2], 3Ah
jne bad_format

mov AL, [input_str + 3]
mov BL, [input_str + 4]

cmp AL, 30h
jl bad_format
cmp AL, 39h
jg bad_format

cmp BL, 30h
jl bad_format
cmp BL, 39h
jg bad_format

sub AX, 30h
sub BX, 30h
mul CX
add AL, BL
mov [minutes], AX

cmp [input_str + 5], 3Ah
jne bad_format

mov AL, [input_str + 6]
mov BL, [input_str + 7]

cmp AL, 30h
jl bad_format
cmp AL, 39h
jg bad_format

cmp BL, 30h
jl bad_format
cmp BL, 39h
jg bad_format

sub AX, 30h
sub BX, 30h
mul CX
add AL, BL
mov [seconds], AX

mov AX, [hours]
cmp [hours], 23h
jg bad_hours
cmp [minutes], 59h
jg bad_minutes
cmp [seconds], 59h
jg bad_seconds

mov AH, 9h
mov DX, offset good_str
int 21h
mov CX, 3600
mov AX, [hours]
mul CX
mov BX, AX
mov AX, [minutes]
mov CX, 60
mul CX
add BX, AX
add BX, [seconds]
push BX
call write_int
jmp exit


bad_len:
  mov AH, 9h
  mov DX, offset error1
  int 21h
  jmp exit

bad_format:
  mov AH, 9h
  mov DX, offset error2
  int 21h
  jmp exit

bad_hours:
  mov AH, 9h
  mov DX, offset error3
  int 21h
  jmp exit

bad_minutes:
  mov AH, 9h
  mov DX, offset error4
  int 21h
  jmp exit

bad_seconds:
  mov AH, 9h
  mov DX, offset error5
  int 21h
  jmp exit

exit:
  mov AH, 4ch
  int 21h
  
	
end main