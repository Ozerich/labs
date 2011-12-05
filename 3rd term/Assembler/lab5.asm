.model small
.stack 1000h

.data 

  offset_x dw 0
  offset_y dw 0
  color db 30

  x dw 0
  y dw 0
  x0 dw 0
  y0 dw 0
  x1 dw ?
  y1 dw ?
  delta dw 0
  error dw 0
  error2 dw ?

  delta_x dw ?
  delta_y dw ?
  sign_x dw ?
  sign_y dw ?

  k_hx dw 1
  k_hy dw 1
  k_wx dw 1
  k_wy dw 1
  
  nx dw ?
  ny dw ?

  radius dw 0
  

  standard_mode db ?
.code
.486

;Save standard graph mode
init_graph_mode proc
    
  mov AH, 0fh
  int 10h
  mov standard_mode, AL

  mov AH, 0
  mov AL, 13h
  int 10h

  ret
init_graph_mode endp


;Restore standard graph mode
close_graph_mode proc

  mov AH, 0
  mov AL, [standard_mode]
  int 10h

  ret
close_graph_mode endp

;Draw point x, y
draw_point proc
  pop BP
  mov AH, 0ch
  mov BH, 0h
  mov AL, [color]
  pop DX
  pop CX
  
  int 10h
  push BP
  ret
draw_point endp

;Draw circle x0, y0, radius
draw_circle proc

  mov [x], 0
  mov AX, [radius]
  mov [y], AX

  mov [delta], 2
  shl AX, 1
  sub [delta], AX
  mov [error], 0

  circle_step:
    
    mov AX, [y]
    cmp AX, 0
    jnge end_circledraw
    
    mov AX, x
    imul k_hx
    cwd
    idiv k_wx
    mov nx, AX
    mov AX, y
    imul k_hy
    cwd
    idiv k_hx
    mov ny, AX

    mov AX, [x0]
    add AX, [nx]
    push AX
    mov AX, [y0]
    add AX, [ny]
    push AX
    call draw_point    
    
    mov AX, [x0]
    sub AX, [nx]
    push AX
    mov AX, [y0]
    add AX, [ny]
    push AX
    call draw_point
    
    mov AX, [x0]
    add AX, [nx]
    push AX
    mov AX, [y0]
    sub AX, [ny]
    push AX  
    call draw_point
  
    mov AX, [x0]
    sub AX, [nx]
    push AX
    mov AX, [y0]
    sub AX, [ny]
    push AX
    call draw_point 

    mov AX, [delta]
    mov [error], AX
    mov AX, [y]
    add [error], AX
    shl [error], 1
    dec [error]

    cmp [error], 0
    jg circle_check2
    cmp [delta], 0
    jge circle_check2

    inc [x]
    mov BX, [x]
    shl BX, 1
    inc BX
    add [delta], BX
    jmp circle_step

    
    circle_check2:

    mov AX, [delta]
    mov [error], AX
    mov AX, [x]
    sub [error], AX
    shl [error], 1
    dec [error]

    cmp [error], 0
    jle circle_endcycle
    cmp [delta], 0
    jle circle_endcycle
    
    
    dec [y]
    mov BX, [y]
    shl BX, 1
    mov AX, 1
    sub AX, BX
    add [delta], AX
    jmp circle_step
    
    circle_endcycle:

    inc [x]
    mov BX, [x]
    sub BX, [y]
    shl BX, 1
    add [delta], BX
    dec [y]
    jmp circle_step

       
  end_circledraw:
  ret
draw_circle endp

abs proc
  pop BP
    
  pop AX
  
  cmp AX, 0
  jge end_abs
  neg AX
  
  end_abs:
  push AX
  push BP
  ret
abs endp

sign proc
  pop BP

  pop AX
  cmp AX, 0
  jge sign_above
  mov AX, -1
  jmp end_sign
  
  sign_above:
  mov AX, 1
  
  end_sign:
  push AX
  push BP
  ret
sign endp

;x0, y0, x1, y1
draw_line proc

  mov AX, [x1]
  sub AX, [x0]
  push AX
  call abs
  pop [delta_x]

  mov BX, [y1]
  sub BX, [y0]
  push BX
  call abs
  pop [delta_y]

  push AX
  call sign
  pop [sign_x]

  push BX
  call sign
  pop [sign_y]

  mov AX, [delta_x]
  sub AX, [delta_y]
  mov [error], AX

  line_step:
    push [x0]
    push [y0]
    call draw_point

    mov AX, [x0]
    cmp AX, [x1]
    jne continue_line
    mov AX, [y0]
    cmp AX, [y1]
    jne continue_line

    jmp exit_line
    
    continue_line:
      mov AX, [error]
      shl AX, 1
      mov [error2], AX	
      
      mov AX, [delta_y]
      neg AX
      cmp [error2], AX
      jng check_line2
      mov AX, [delta_y]
      sub [error], AX
      mov AX, [sign_x]
      add [x0], AX

      check_line2:
        mov AX, [delta_x]
        cmp [error2], AX
        jnl line_step
        mov AX, [delta_x]
        add [error], AX
        mov AX, [sign_y]
        add [y0], AX
      
      jmp line_step

    exit_line:
      ret

draw_line endp

main:
  mov AX, @data
  mov DS, AX

  call init_graph_mode

  mov [color], 50
  
  mov [x0], 160
  mov [y0], 100
  mov [radius], 90
  call draw_circle


  mov [x0], 160
  mov [y0], 100
  mov [radius], 35
  mov [k_hx], 5
  mov [k_hy], 4
  mov [k_wx], 2
  mov [k_wy], 2
  call draw_circle

  mov [color], 60

  mov [x0], 203
  mov [y0], 78
  mov [x1], 216
  mov [y1], 120
  call draw_line

  mov [x0], 160
  mov [y0], 10
  mov [x1], 203
  mov [y1], 78
  call draw_line

  mov [x0], 160
  mov [y0], 10
  mov [x1], 216
  mov [y1], 120
  call draw_line

  mov [x0], 104
  mov [y0], 120
  mov [x1], 117
  mov [y1], 78
  call draw_line

  mov [x0], 117
  mov [y0], 78
  mov [x1], 160
  mov [y1], 10
  call draw_line

  mov [x0], 104
  mov [y0], 120
  mov [x1], 160
  mov [y1], 10
  call draw_line

  mov [x0], 117
  mov [y0], 78
  mov [x1], 203
  mov [y1], 78
  call draw_line

  mov [x0], 104
  mov [y0], 120
  mov [x1], 216
  mov [y1], 120
  call draw_line


  mov AH, 01h
  int 21h

  call close_graph_mode

  mov AH, 4ch
  int 21h	

end main