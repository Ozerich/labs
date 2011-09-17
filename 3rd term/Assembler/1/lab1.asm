.model small
.stack 100h
.data
	a dw 5
	b dw 2
	cc dw 1
	d dw 4
	max dw ?
.code
main:
	mov AX, @data
	mov DS, AX
	mov AX, a
	mul a
	mul a
	mul a
	cmp AX, b
	jg yes1
	mov AX, cc
	mul cc
	mul cc
	add AX, b
	jmp exit
yes1:
	mov AX, cc
	mul b
	mov BX, AX
	mov AX, d
	div b
	cmp BX, AX
	je yes2
	mov AX, a
	mov BX, b
	mov CX, cc
	cmp AX, BX
	jg max_a
	mov max, BX
find_max2:
	cmp CX, max
	jg max_c
found_max:
	mov AX, a
	mul a
	sub AX, b
	mov BX, AX
	mov AX, max
	div BX
	jmp exit
max_a:
	mov max, AX 
	jmp find_max2
max_c:
	mov max, CX
	jmp found_max
yes2:
	mov AX, a
	or AX, b
	jmp exit	
exit:
	mov AH, 4ch
	int 21h
end main