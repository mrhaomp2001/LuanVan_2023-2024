<?php

namespace App\Livewire\Admins\Classrooms;

use App\Models\Classroom;
use Illuminate\Support\Facades\Storage;
use Livewire\Component;
use Livewire\WithFileUploads;
class AdminClassroomsShow extends Component
{
    use WithFileUploads;
    public $id;
    public $name;
    public $description;
    public $theme_color;
    public $is_open;
    public $image_path;
    public $questionCollections;
    public $image;
    public $classroom;

    public function mount($id)
    {
        
        $this->classroom = Classroom::findOrFail($id);

        if ($this->classroom->user_id != auth()->user()->id) {
            return redirect(route("404"));
        }

        $this->id = $id;
        $this->name = $this->classroom->name;
        $this->description = $this->classroom->description;
        $this->theme_color = $this->classroom->theme_color;
        $this->is_open = $this->classroom->is_open;
        $this->image_path = $this->classroom->image_path;

        $this->questionCollections = $this->classroom->questionCollections;
    }

    public function save()
    {
        $this->validate(
            [
                'name' => ["required", "min:3", 'max:64'],
                'description' => ["required", "min:3", 'max:512'],
                "theme_color" => ["required"],
                'image' => ['image']
            ],
            [
                'name.required' => "Cần nhập tên",
                'name.min' => "Cần nhập tên với tối thiểu :min ký tự",
                'name.max' => "Cần nhập tên với tối đa :max ký tự",

                'description.required' => "Cần nhập miêu tả",
                'description.min' => "Cần nhập miêu tả với tối thiểu :min ký tự",
                'description.max' => "Cần nhập miêu tả với tối đa :max ký tự",

                'theme_color.required' => "Cần nhập màu",
                'image.image' => "Cần nhập hình ảnh"
            ]
        );

        $classroom = Classroom::findOrFail($this->id);

        $classroom->name = $this->name;
        $classroom->description = $this->description;
        $classroom->theme_color = $this->theme_color;



        if ($this->is_open == "true") {
            $classroom->is_open = true;
        } else {
            $classroom->is_open = false;
        }

        $classroom->save();

        if (isset($this->delete_image)) {
            if ($this->delete_image == "true") {
                Storage::disk('public')->delete("classrooms/avatars/" . $this->id . '.png');

            }
        } else {
            if (isset($this->image)) {
                Storage::disk('public')->putFileAs("classrooms/avatars/", $this->image, $this->id . '.png');
            }
        }

        $this->redirect(route("classrooms.index", ['id' => $this->id]));
    }
    public function render()
    {
        return view('livewire.admins.classrooms.admin-classrooms-show');
    }
}
