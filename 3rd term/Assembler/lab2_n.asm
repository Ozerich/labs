;For Bogdan Nechaev 
;Sort words in file and write result to file

.model small
.stack 100h

.data
  str_infile db 'Input file: ', 10, 13, '$'
  str_outfile db 'Output file: ', 10, 13, '$'

  str_file_open_error db 'File open Error!', 10, 13, 0

  input_str db 256 dup(0)
  file_text db 1000 dup(0)

  result_text db 1000 dup(0)
  result_ind dw 0

  input_start dw 0

  file_handle dw ?
  
  
  infile db 256 dup(0)
  outfile db 256 dup(0)

  delimetrs db ' ,.:()',0
  delimetrs_len dw 6

  word_1 db 20 dup(0)
  word_2 db 20 dup(0)

  ind_1 dw 0
  ind_2 dw 0

  current_word_2_pos dw ?
  old_word_2_textpos dw ?
  old_word_1 db 20 dup(0)
  
  word_1_len dw 0
  word_2_len dw 0

.code

read_str proc
  pop BP
  pop BX

  mov input_start, BX
  
  read_schar:

    mov AH, 01h
    int 21h

    cmp AL, 13
    je end_sread

    mov [BX], AL
    inc BX
    jmp read_schar
  
  end_sread:

    mov [BX], '$'
    sub BX, [input_start]

    push BP
    ret
read_str endp


input_filenames proc

  mov ah, 9h
  mov dx, offset str_infile
  int 21h
  
  mov ax, offset infile
  push ax
  call read_str
  
  mov ah, 9h
  mov dx, offset str_outfile
  int 21h

  mov ax, offset outfile
  push ax

  call read_str
  
  ret
input_filenames endp

read_file proc

  mov ax, 3d00h
  mov dx, offset infile
  int 21h
  jc open_error
  mov file_handle, ax

  mov ah, 3fh
  mov bx, file_handle
  mov cx, 1000
  mov dx, offset file_text
  int 21h
  mov ax, cx

  mov ah, 3eh
  mov bx, file_handle
  int 21h

  ret 

open_error:

  mov ah, 9h
  mov dx, offset str_file_open_error
  int 21h

  ret    
read_file endp

clear_word_1 proc
  
  xor di, di
  
  clear_word_1_cycle:
      
      cmp [word_1 + di], 0 
      je exit_clear_word_1
      mov [word_1 + di], 0
      inc di
      jmp clear_word_1_cycle

exit_clear_word_1:
  ret
clear_word_1 endp

clear_word_2 proc
  
  xor di, di
  
  clear_word_2_cycle:
      
      cmp [word_2 + di], 0 
      je exit_clear_word_2
      mov [word_2 + di], 0
      inc di
      jmp clear_word_2_cycle

exit_clear_word_2:
  ret
clear_word_2 endp

clear_old_word_1 proc
  
  xor di, di
  
  clear_old_word_1_cycle:
      
      cmp [old_word_1  + di], 0 
      je exit_old_word_1
      mov [old_word_1  + di], 0
      inc di
      jmp clear_old_word_1_cycle

exit_old_word_1:
  ret
clear_old_word_1 endp


is_delimetr proc
  pop bp
  pop ax
  
  cld
  lea di, delimetrs
  mov cx, [delimetrs_len]
  
  repne scas delimetrs
  je found
  
  mov ax, 0
  jmp exit_is_delimetr
  
  found:
    mov ax, 1

  exit_is_delimetr:
    push ax
  push bp
  ret
is_delimetr endp

trim proc
  pop bp
  pop si
  dec si

  trim_cycle:
    inc si
    mov al, [file_text + si]
    push bp
    push ax
    call is_delimetr
    pop ax
    pop bp
    cmp al, 0
    jne trim_cycle

  push si
  push bp
  ret
trim endp

check_for_swap proc

  cmp [word_2], 0
  je exit_check_for_swap

  lea si, word_1
  lea di, word_2

  mov cx, 20
  
  repe  cmps word_1, word_2
  jbe exit_check_for_swap

  cld

  
  mov dx, [current_word_2_pos]
  mov [old_word_2_textpos], dx
  
  lea di, word_1
  lea si, word_2

  rep movs word_1, word_2

exit_check_for_swap:  
  ret
check_for_swap endp

final_swap proc
  
  xor si, si
  
  final_swap_cycle_1:
    inc si
    mov al, [old_word_1 + si]
    cmp al, 0
    jne final_swap_cycle_1
  
  mov [word_1_len], si
  
  mov si, old_word_2_textpos 
  cmp si,0
  je exit_final_swap
  xor di, di

  final_swap_cycle_2:
    inc di
    inc si
    xor ax, ax
    mov al, byte ptr [file_text + si]
    cmp al, 0
    je set_word_2_len
    push bp
    push di
    push ax
    call is_delimetr
    pop ax
    pop di
    pop bp
    cmp ax, 1
    jne final_swap_cycle_2
    
    set_word_2_len:

      mov [word_2_len], di

  clear_cycle:
    dec si
    mov [file_text + si], 0
    dec di
    cmp di, 0
    jnz clear_cycle
	
  mov si, [word_1_len]
  mov [old_word_1 + si], 20h
  inc si
  mov [word_1_len], si
  

  mov si, [word_1_len]
  mov di, [word_2_len]
  cmp si, di
  jna do_fill
  
  mov ax, si
  sub ax, di

  mov si, 999
  sub si, ax
  

  sdvig_cycle:
    dec si
    mov di, si
    add di, ax
    
    sdvig_in_cycle:
      mov cl, [file_text + di]
      inc di
      mov [file_text + di], cl
      sub di, 2
      cmp di, si
      jge sdvig_in_cycle

    cmp si, old_word_2_textpos
    jne sdvig_cycle
	
  

  do_fill:
    
    mov si,old_word_2_textpos
    mov di, 0
    
    do_fill_cycle:
    
      mov al, [old_word_1 + di]
      mov [file_text + si], al
      inc di
      inc si
      cmp [old_word_1 + di], 0
      jne do_fill_cycle
	
	cmp di, [word_2_len]
	jae exit_final_swap
	
	fill_spaces_cycle:
		inc di
		mov [file_text + si], 20h
		inc si
		cmp di, [word_2_len]
		jne fill_spaces_cycle
	  
exit_final_swap:
  ret
final_swap endp

sort_file proc

cycle_1:

  mov si, [ind_1]
  mov bl, [file_text + si]
  cmp bl, 0
  je exit_sort

  push si
  call trim
  pop si

  call clear_old_word_1

  xor di, di
  
  first_word_cycle:
	
    mov bl, [file_text + si]
    mov [word_1 + di], bl
    mov [old_word_1 + di], bl
    inc di
    inc si
    mov [ind_1], si
    cmp bl, 0
    je end_cycle_2
    mov bl, [file_text + si]
    push bp
    push di
    push bx
    call is_delimetr
    pop bx
    pop di
    pop bp
    cmp bx, 0
    je first_word_cycle

	mov [old_word_2_textpos], 0
	
  cycle_2:

    push si
    call trim
    pop si

    mov [ind_2], si
    call clear_word_2

    xor di, di

    mov [current_word_2_pos], si
    get_next_word:

      mov bl, [file_text + si]
      mov [word_2 + di], bl
      cmp bl, 0
      je finish_cycle_2
      inc si
      inc di
      mov bl, [file_text + si]
      push bp
      push di
      push bx
      call is_delimetr
      pop bx
      pop di
      pop bp
      cmp bx, 0
      je get_next_word

   finish_cycle_2:

      push bx	    
      push si
      call check_for_swap
      pop si
      pop bx
      cmp bl, 0
      je end_cycle_2
      jmp cycle_2

    end_cycle_2:

      mov di, [result_ind]
      xor si, si
    

  word_1_to_result:
    
    mov al, [word_1 + si]
    cmp al, 0
    je finish_swap
    mov [result_text + di], al
    inc si
    inc di
    jmp word_1_to_result

  finish_swap:
  
    mov [result_text + di], 20h
    inc di
    mov [result_ind], di
    mov ax, [result_ind]
    add ax, si
    push ax
    call final_swap
    pop ax
	call clear_word_1
    jmp cycle_1
    

  exit_sort:
    ret
sort_file endp

write_file proc
  
  xor ax, ax

  mov ah, 3ch
  mov cx, 0
  mov dx, offset outfile
  int 21h
  mov file_handle, ax

  mov ah, 40h
  mov bx, file_handle
  mov cx, result_ind
  lea dx, result_text
  int 21h

  ret
write_file endp

main:
  mov ax, @data
  mov ds, ax
  mov es, ax

  call input_filenames
  call read_file
  call sort_file
  call write_file

  mov ah, 4ch
  int 21h

end main