package main

import "testing"

/*
	1. Test files end inm _test.go
	2. Test functions start with Test... and accept t *testing.T
	2a. Go supplies 't', which lets us report failures. It's a pointer!
	3. 'go test' discovers and runs these functions  
*/

func TestRecipeLabel(t *testing.T) {
	got := recipeLabel("Chicken curry", "Ma", "Khmer")
	want := "Chicken curry - told by Ma (Khmer)"

	if got != want {
		t.Errorf("recipeLabel() = %q, want %q", got, want)
	}
}

func TestRecipeLabelWithDifferentInputs(t *testing.T) {
	got := recipeLabel("Egusi soup", "Anthony", "Igbo")
	want := "Egusi soup - told by Anthony (Igbo)"

	if got != want {
		t.Errorf("recipeLabel() outputted this: %q, we want: \n %q", got, want)
	}
}