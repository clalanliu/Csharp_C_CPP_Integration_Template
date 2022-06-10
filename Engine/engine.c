#include <stdio.h>
#include <stdlib.h>
#include "engine.h"

double* AddTwoArrayWIthLenth3(double* a, double* b, double** sum) {

	double* tmp = malloc(sizeof(double) * 3);
	for (int i = 0; i < 3; i++)
	{
		tmp[i] = a[i] + b[i];
	}
	(*sum) = tmp;
	return (*sum);
}
