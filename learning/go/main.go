package main

import "fmt"

func main() {
	dish := "Chicken curry"
	speaker := "Ma"
	language := "Khmer"

	label := recipeLabel(dish, speaker, language)
	fmt.Println(label)
}

func recipeLabel(dish string, speaker string, language string) string {
	return dish + " - told by " + speaker + " (" + language + ")"
}
