	.file	"ascii.c"
	.section	.rodata
.LC0:
	.string	"%03d %x%x %c "
	.text
	.globl	main
	.type	main, @function
main:
.LFB0:
	.cfi_startproc
	pushq	%rbp
	.cfi_def_cfa_offset 16
	.cfi_offset 6, -16
	movq	%rsp, %rbp
	.cfi_def_cfa_register 6
	subq	$16, %rsp
	movl	$0, -8(%rbp)
	jmp	.L2
.L6:
	movl	$0, -4(%rbp)
	jmp	.L3
.L5:
	movl	-8(%rbp), %eax
	sall	$4, %eax
	movl	%eax, %edx
	movl	-4(%rbp), %eax
	addl	%edx, %eax
	movb	%al, -9(%rbp)
	movzbl	-9(%rbp), %eax
	movsbl	%al, %ecx
	movl	-8(%rbp), %eax
	sall	$4, %eax
	movl	%eax, %edx
	movl	-4(%rbp), %eax
	leal	(%rdx,%rax), %esi
	movl	-4(%rbp), %edx
	movl	-8(%rbp), %eax
	movl	%ecx, %r8d
	movl	%edx, %ecx
	movl	%eax, %edx
	movl	$.LC0, %edi
	movl	$0, %eax
	call	printf
	cmpl	$7, -4(%rbp)
	jne	.L4
	movl	$10, %edi
	call	putchar
.L4:
	addl	$1, -4(%rbp)
.L3:
	cmpl	$15, -4(%rbp)
	jle	.L5
	movl	$10, %edi
	call	putchar
	addl	$1, -8(%rbp)
.L2:
	cmpl	$15, -8(%rbp)
	jle	.L6
	movl	$0, %eax
	leave
	.cfi_def_cfa 7, 8
	ret
	.cfi_endproc
.LFE0:
	.size	main, .-main
	.ident	"GCC: (Ubuntu 5.4.0-6ubuntu1~16.04.2) 5.4.0 20160609"
	.section	.note.GNU-stack,"",@progbits
